
using EcoIndicators.Data;
using EcoIndicators.Models.MakStat;
using EcoIndicators.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoIndicators.Services.MakStat.Indicators.Waste.Loaders
{
    public class WasteBySite : IWaste_by_site_of_generation
    {
        private readonly IApiClient _client;
        private readonly AppDbContext _db;

        public WasteBySite(IApiClient client, AppDbContext db)
        {
            _client = client;
            _db = db;
        }

        public async Task Load()
        {
            string query = @"{
              ""query"": [],
              ""response"": {
                ""format"": ""json""
              }
            }";

            string url = "https://makstat.stat.gov.mk:443/PXWeb/api/v1/en/MakStat/ZivotnaSredina/Otpad/220_ZivSr_MK_KomOtpmessozd_ml.px";
            var apiResponse = await _client.FetchData(url, query);

            var records = Transform(apiResponse);

            foreach (var r in records)
            {
                bool exists = await _db.Waste_by_site_of_generations.AnyAsync(x => x.Year == r.Year);
                if (!exists) _db.Waste_by_site_of_generations.Add(r);
            }

            await _db.SaveChangesAsync();
        }

        public List<Waste_by_site_of_generation> Transform(ApiResponse api)
        {
            if (api?.Data == null || api.Data.Count == 0)
                return new List<Waste_by_site_of_generation>();
            var dict = new Dictionary<int, Waste_by_site_of_generation>();
            foreach (var item in api.Data)
            {
                if (item.Key.Count < 2 || item.Values.Count == 0)
                    continue;
                if (!int.TryParse(item.Key[1], out int year))
                    continue;
                string sectorCode = item.Key[0];

                if (!decimal.TryParse(item.Values[0], out decimal value))
                    continue;

                if (!dict.ContainsKey(year))
                {
                    dict[year] = new Waste_by_site_of_generation { Year = year };
                }

                var row = dict[year];

                switch (sectorCode)
                {
                    case "0": row.Total = value; break;
                    case "1": row.Households = value; break;
                    case "2": row.Commercial_waste = value; break;
                }
            }
                return dict.Values.ToList();
        }
    }
}
