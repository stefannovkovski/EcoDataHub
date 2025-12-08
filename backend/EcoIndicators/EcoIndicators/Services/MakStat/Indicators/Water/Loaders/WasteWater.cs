
using EcoIndicators.Data;
using EcoIndicators.Models.MakStat;
using EcoIndicators.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoIndicators.Services.MakStat.Indicators.Water.Loaders
{
    public class WasteWater : IWasteWater
    {
        private readonly IApiClient _client;
        private readonly AppDbContext _db;

        public WasteWater(IApiClient client, AppDbContext db)
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

            string url = "https://makstat.stat.gov.mk:443/PXWeb/api/v1/en/MakStat/ZivotnaSredina/Voda/631_ZivSred_Mk_JavKan_ml.px";
            var apiResponse = await _client.FetchData(url, query);

            var records = Transform(apiResponse);

            foreach (var r in records)
            {
                bool exists = await _db.Waste_waters.AnyAsync(x => x.Year == r.Year);
                if (!exists) _db.Waste_waters.Add(r);
            }

            await _db.SaveChangesAsync();
        }

        public List<Waste_water> Transform(ApiResponse api)
        {
            if (api?.Data == null || api.Data.Count == 0)
                return new List<Waste_water>();
            var dict = new Dictionary<int, Waste_water>();
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
                    dict[year] = new Waste_water { Year = year };
                }
                 
                var row = dict[year];

                switch (sectorCode)
                {
                    case "1": row.Total = value; break;
                    case "2": row.From_households = value; break;
                    case "3": row.From_the_economy = value; break;
                    case "4": row.From_other_users = value; break;
                    case "5": row.From_own_consumption = value; break;
                }
    }
            return dict.Values.ToList();
        }
    }
}
