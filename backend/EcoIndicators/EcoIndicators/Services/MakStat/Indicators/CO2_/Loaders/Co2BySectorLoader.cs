using EcoIndicators.Data;
using EcoIndicators.Models;
using EcoIndicators.Models.MakStat;
using Microsoft.EntityFrameworkCore;

// Проекции на емисија на стакленички гасови по сектори во CO2 -eквивалентно [kt] (Основно сценарио), по години

namespace EcoIndicators.Services.MakStat.Indicators.CO2.Loaders
{
    public class Co2BySectorLoader : ICo2BySectorLoader
    {
        private readonly IApiClient _client;
        private readonly AppDbContext _db;

        public Co2BySectorLoader(IApiClient client, AppDbContext db)
        {
            _client = client;
            _db = db;
        }

        public async Task Load()
        {
            string query = @"
            {
              ""query"": [
                {
                  ""code"": ""Sectors"",
                  ""selection"": {
                    ""filter"": ""item"",
                    ""values"": [""01"", ""02"", ""03"", ""04"", ""05"", ""06"", ""07""]
                  }
                }
              ],
              ""response"": {
                ""format"": ""JSON""
              }
            }";

            string url = "https://makstat.stat.gov.mk:443/PXWeb/api/v1/mk/MakStat/ZivotnaSredina/Vozduh/275_ZivSr_nac_stak_gas_proekcii_ml.px";
            var apiResponse = await _client.FetchData(url,query);

            var records = Transform(apiResponse);

            foreach (var r in records)
            {
                bool exists = await _db.SectorCO2s.AnyAsync(x => x.Year == r.Year);
                if (!exists) _db.SectorCO2s.Add(r);
            }

            await _db.SaveChangesAsync();
        }
        public List<SectorCO2> Transform(ApiResponse api)
        {
            if (api?.Data == null || api.Data.Count == 0)
                return new List<SectorCO2>();
            var dict = new Dictionary<int, SectorCO2>();
            foreach (var item in api.Data)
            {
                if (item.Key.Count < 2 || item.Values.Count == 0)
                    continue;
                if (!int.TryParse(item.Key[0], out int year))
                    continue;
                string sectorCode = item.Key[1];

                if (!decimal.TryParse(item.Values[0], out decimal value))
                    continue;

                if (!dict.ContainsKey(year))
                {
                    dict[year] = new SectorCO2 { Year = year };
                }

                var row = dict[year];

                switch (sectorCode)
                {
                    case "01": row.Energy = value; break;
                    case "02": row.Heat = value; break;
                    case "03": row.Transport = value; break;
                    case "04": row.Industrial_processes = value; break;
                    case "05": row.Waste = value; break;
                    case "06": row.Agriculture = value; break;
                    case "07": row.Total = value; break;
                }
            }

            return dict.Values.ToList();
        }
    }

}
