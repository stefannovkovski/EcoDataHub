
using EcoIndicators.Data;
using EcoIndicators.Models.MakStat;
using EcoIndicators.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoIndicators.Services.MakStat.Indicators.Waste.Loaders
{
    public class AmountMunicipalWaste : IAmountMunicipalWaste
    {
        private readonly IApiClient _client;
        private readonly AppDbContext _db;

        public AmountMunicipalWaste(IApiClient client, AppDbContext db)
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

            string url = "https://makstat.stat.gov.mk:443/PXWeb/api/v1/en/MakStat/ZivotnaSredina/Otpad/200_ZivSr_MK_KomOtp_ml.px";
            var apiResponse = await _client.FetchData(url, query);

            var records = Transform(apiResponse);

            foreach (var r in records)
            {
                bool exists = await _db.Amount_of_collected_municipal_wastes.AnyAsync(x => x.Year == r.Year);
                if (!exists) _db.Amount_of_collected_municipal_wastes.Add(r);
            }

            await _db.SaveChangesAsync();
        }

        public List<Amount_of_collected_municipal_waste> Transform(ApiResponse api)
        {
            if (api?.Data == null || api.Data.Count == 0)
                return new List<Amount_of_collected_municipal_waste>();
            var dict = new Dictionary<int, Amount_of_collected_municipal_waste>();
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
                    dict[year] = new Amount_of_collected_municipal_waste { Year = year };
                }

                var row = dict[year];

                switch (sectorCode)
                {
                    case "0": row.Total = value; break;
                    case "1": row.Paper = value; break;
                    case "2": row.Glass = value; break;
                    case "3": row.Plastic = value; break;
                    case "4": row.Metal_iron_steel_aluminum = value; break;
                    case "5": row.Organic_waste_food_leaves = value; break;
                    case "6": row.Textile = value; break;
                    case "7": row.Rubber = value; break;
                    case "8": row.Mixed_municipal_waste = value; break;
                    case "9": row.Other = value; break;
                }
    }
            return dict.Values.ToList();
        }
    }
}
