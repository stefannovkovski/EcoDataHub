using EcoIndicators.Data;
using EcoIndicators.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoIndicators.Services.MakStat.Indicators.CO2.Loaders
{
    public class TotalEmissionSO2Loader : ITotalEmissionSO2Loader
    {
        private readonly IApiClient _client;
        private readonly AppDbContext _db;

        public TotalEmissionSO2Loader(IApiClient client, AppDbContext db)
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

            string url = "https://makstat.stat.gov.mk:443/PXWeb/api/v1/mk/MakStat/ZivotnaSredina/Vozduh/450_ZivSred_MK_emiSO2_ml.px";
            var apiResponse = await _client.FetchData(url, query);

            var records = Transform(apiResponse);

            foreach (var r in records)
            {
                bool exists = await _db.TotalEmissionSO2s.AnyAsync(x => x.Year == r.Year);
                if (!exists) _db.TotalEmissionSO2s.Add(r);
            }

            await _db.SaveChangesAsync();
        }
        public List<TotalEmissionSO2> Transform(ApiResponse api, int startYear = 2006)
        {
            if (api?.Data == null || api.Data.Count == 0)
                return new List<TotalEmissionSO2>();
            var dict = new Dictionary<int, TotalEmissionSO2>();
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
                    dict[year] = new TotalEmissionSO2 { Year = year };
                }

                var row = dict[year];

                switch (sectorCode)
                {
                    case "0": row.Total = value; break;
                    case "1": row.Combustion_processes = value; break;
                    case "2": row.Production_processes = value; break;
                    case "3": row.Transport = value; break;
                    case "4": row.Other = value; break;
                }
            }

            return dict.Values.ToList();
        }
    }
}
