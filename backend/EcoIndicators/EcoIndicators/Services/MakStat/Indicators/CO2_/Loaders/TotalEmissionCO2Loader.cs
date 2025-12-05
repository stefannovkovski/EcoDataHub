using EcoIndicators.Data;
using EcoIndicators.Models;
using EcoIndicators.Models.MakStat;
using Microsoft.EntityFrameworkCore;

// Вкупна емисија на стакленички гасови, CO2 - еквивалентно (kt)

namespace EcoIndicators.Services.MakStat.Indicators.CO2.Loaders
{
    public class TotalEmissionCO2Loader : ITotalEmissionCO2Loader
    {
        private readonly IApiClient _client;
        private readonly AppDbContext _db;

        public TotalEmissionCO2Loader(IApiClient client, AppDbContext db)
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

            string url = "https://makstat.stat.gov.mk:443/PXWeb/api/v1/mk/MakStat/ZivotnaSredina/Vozduh/425_ZivSred_MK_CO2GHG_ml.px";
            var apiResponse = await _client.FetchData(url, query);

            var records = Transform(apiResponse);

            foreach (var r in records)
            {
                bool exists = await _db.TotalEmissionCO2s.AnyAsync(x => x.Year == r.Year);
                if (!exists) _db.TotalEmissionCO2s.Add(r);
            }

            await _db.SaveChangesAsync();
        }
        public List<TotalEmissionCO2> Transform(ApiResponse api, int startYear = 2003)
        {
            var list = new List<TotalEmissionCO2>();

            if (api?.Data == null || api.Data.Count == 0)
                return list;

            foreach (var item in api.Data)
            {
                if (item.Values.Count == 0)
                    continue;

                if (!int.TryParse(item.Key[0], out int index))
                    continue;

                if (!decimal.TryParse(item.Values[0], out decimal value))
                    continue;

                list.Add(new TotalEmissionCO2
                {
                    Year = startYear + index,
                    Value = value
                });
            }

            return list;
        }
    }
}
