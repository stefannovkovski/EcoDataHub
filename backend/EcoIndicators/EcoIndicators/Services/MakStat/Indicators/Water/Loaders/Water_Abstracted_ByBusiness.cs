using EcoIndicators.Data;
using EcoIndicators.Models.MakStat;
using EcoIndicators.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoIndicators.Services.MakStat.Indicators.Water.Loaders
{
    public class Water_Abstracted_ByBusiness : IWater_abstracted_by_business
    {
        private readonly IApiClient _client;
        private readonly AppDbContext _db;

        public Water_Abstracted_ByBusiness(IApiClient client, AppDbContext db)
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

            string url = "https://makstat.stat.gov.mk:443/PXWeb/api/v1/en/MakStat/ZivotnaSredina/Voda/630_ZivSred_MK_Voda_dejnost_ml.px";
            var apiResponse = await _client.FetchData(url, query);

            var records = Transform(apiResponse);

            foreach (var r in records)
            {
                bool exists = await _db.Water_abstracted_by_business_entitiess.AnyAsync(x => x.Year == r.Year);
                if (!exists) _db.Water_abstracted_by_business_entitiess.Add(r);
            }

            await _db.SaveChangesAsync();
        }

        public List<Water_abstracted_by_business_entities> Transform(ApiResponse api)
        {
            if (api?.Data == null || api.Data.Count == 0)
                return new List<Water_abstracted_by_business_entities>();
            var dict = new Dictionary<string, Water_abstracted_by_business_entities>();
            foreach (var item in api.Data)
            {
                if (item.Key.Count < 3 || item.Values.Count == 0)
                    continue;
                if (!int.TryParse(item.Key[0], out int year))
                    continue;
                if (!int.TryParse(item.Key[1], out int indicatorCode))
                    continue;
                if (!decimal.TryParse(item.Values[0], out decimal value))
                    continue;
                string sectorCode = item.Key[2];
                string key = year.ToString() + indicatorCode.ToString();
                string type="";
                switch (indicatorCode)
                {
                    case 0: type = "Total"; break;
                    case 1: type = "Ground water"; break;
                    case 2: type = "Springs"; break;
                    case 3: type = "Surface water"; break;
                    case 4: type = "Public water supply"; break;
                    case 5: type = "Other"; break;
                }

                if (!dict.ContainsKey(key))
                {
                    dict[key] = new Water_abstracted_by_business_entities { Year = year, WaterSourceType = type };
                }

                var row = dict[key];

                switch (sectorCode)
                {
                    case "1": row.Total = value; break;
                    case "2": row.MiningAndQuarrying = value; break;
                    case "3": row.ManufacturingIndustry = value; break;
                    case "4": row.ElectricityGasSupply = value; break;
                    case "5": row.AgricultureForestryFishing = value; break;
                    case "6": row.WaterSupplyWasteManagement = value; break;
                    case "7": row.Construction = value; break;
                }

                }
            return dict.Values.ToList();
        }
    }
}
