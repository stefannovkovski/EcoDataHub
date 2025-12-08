using EcoIndicators.Data;
using EcoIndicators.Models.MakStat;
using EcoIndicators.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoIndicators.Services.MakStat.Indicators.Waste.Loaders
{
    public class CollectedAndGeneratedWaste : ICollected_and_generated_municipal_waste
    {
        private readonly IApiClient _client;
        private readonly AppDbContext _db;

        public CollectedAndGeneratedWaste(IApiClient client, AppDbContext db)
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

            string url = "https://makstat.stat.gov.mk:443/PXWeb/api/v1/en/MakStat/ZivotnaSredina/Otpad/325_ZivSr_reg_08_11_KomOtp_ml.px";
            var apiResponse = await _client.FetchData(url, query);

            var records = Transform(apiResponse);

            foreach (var r in records)
            {
                bool exists = await _db.Collected_and_generated_municipal_wastes.AnyAsync(x => x.Year == r.Year);
                if (!exists) _db.Collected_and_generated_municipal_wastes.Add(r);
            }

            await _db.SaveChangesAsync();
        }

        public List<Collected_and_generated_municipal_waste> Transform(ApiResponse api)
        {
            if (api?.Data == null || api.Data.Count == 0)
                return new List<Collected_and_generated_municipal_waste>();
            var dict = new Dictionary<string, Collected_and_generated_municipal_waste>();
            foreach (var item in api.Data)
            {
                if (item.Key.Count < 3 || item.Values.Count == 0)
                    continue;
                if (!int.TryParse(item.Key[1], out int year))
                    continue;
                if (!decimal.TryParse(item.Key[2], out decimal indicatorCode))
                    continue;
                if (!decimal.TryParse(item.Values[0], out decimal value))
                    continue;
                string sectorCode = item.Key[0];

                string key = year.ToString() + indicatorCode.ToString();
                string type = "";
                switch (indicatorCode)
                {
                    case 0: type = "Collected municipal waste"; break;
                    case 1: type = "Generated municipal waste"; break;
                }

                if (!dict.ContainsKey(key))
                {
                    dict[key] = new Collected_and_generated_municipal_waste { Year = year, Type = type };
                }

                var row = dict[key];

                switch (sectorCode)
                {
                    case "000": row.Total = value; break;
                    case "001": row.Vardar = value; break;
                    case "002": row.East = value; break;
                    case "003": row.Southwest = value; break;
                    case "004": row.Southeast = value; break;
                    case "005": row.Pelagonia = value; break;
                    case "006": row.Polog = value; break;
                    case "007": row.Northeast = value; break;
                    case "008": row.Skopje = value; break;
                }
              
            }
            return dict.Values.ToList();
        }
    }
}
