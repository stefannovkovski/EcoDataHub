
using EcoIndicators.Data;
using EcoIndicators.Models;
using EcoIndicators.Models.MakStat;
using Microsoft.EntityFrameworkCore;

namespace EcoIndicators.Services.MakStat.Indicators.Water.Loaders
{
    public class WaterForProductionPurposes : IWaterForProductionPurposes
    {
        private readonly IApiClient _client;
        private readonly AppDbContext _db;

        public WaterForProductionPurposes(IApiClient client, AppDbContext db)
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

            string url = "https://makstat.stat.gov.mk:443/PXWeb/api/v1/en/MakStat/ZivotnaSredina/Voda/600_ZivSred_MK_VODKoris_ml.px";
            var apiResponse = await _client.FetchData(url, query);

            var records = Transform(apiResponse);

            foreach (var r in records)
            {
                bool exists = await _db.Water_For_Productions.AnyAsync(x => x.Year == r.Year);
                if (!exists) _db.Water_For_Productions.Add(r);
            }

            await _db.SaveChangesAsync();
        }

        public List<Water_For_Production> Transform(ApiResponse api, int startYear = 2008)
        {
            if (api?.Data == null || api.Data.Count == 0)
                return new List<Water_For_Production>();
            var dict = new Dictionary<int, Water_For_Production>();
            foreach (var item in api.Data)
            {
                if (item.Key.Count < 2 || item.Values.Count == 0)
                    continue;
                if (!int.TryParse(item.Key[0], out int year))
                    continue;
                string sectorCode = item.Key[1];

                if (!decimal.TryParse(item.Values[0], out decimal value))
                    continue;

                year += startYear;
                if (!dict.ContainsKey(year))
                {
                    dict[year] = new Water_For_Production { Year = year };
                }

                var row = dict[year];

                switch (sectorCode)
                {
                    case "1": row.Total = value; break;
                    case "2": row.Fresh_water_tech = value; break;
                    case "3": row.Fresh_drinking = value; break;
                    case "4": row.Total_recirculation_water = value; break;
                    case "5": row.Recurculation_fresh_water_added = value; break;
                    case "6": row.Reused_water_afterPurifying = value; break;
                    case "7": row.Reused_water_afterCooling = value; break;
                 }
            }
            return dict.Values.ToList();
        }
    }
}
