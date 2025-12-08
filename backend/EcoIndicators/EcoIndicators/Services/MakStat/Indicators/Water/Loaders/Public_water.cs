
using EcoIndicators.Data;
using EcoIndicators.Models;
using EcoIndicators.Models.MakStat;
using Microsoft.EntityFrameworkCore;

namespace EcoIndicators.Services.MakStat.Indicators.Water.Loaders
{
    public class Public_water : IPublic_water_supply
    {
    private readonly IApiClient _client;
    private readonly AppDbContext _db;

    public Public_water(IApiClient client, AppDbContext db)
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

        string url = "https://makstat.stat.gov.mk:443/PXWeb/api/v1/en/MakStat/ZivotnaSredina/Voda/630_ZivSred_Mk_JavVod_ml.px";
        var apiResponse = await _client.FetchData(url, query);

        var records = Transform(apiResponse);

        foreach (var r in records)
        {
            bool exists = await _db.Public_water_supplys.AnyAsync(x => x.Year == r.Year);
            if (!exists) _db.Public_water_supplys.Add(r);
        }

        await _db.SaveChangesAsync();
    }

        public List<Public_water_supply> Transform(ApiResponse api)
        {
            if (api?.Data == null || api.Data.Count == 0)
                return new List<Public_water_supply>();
            var dict = new Dictionary<int, Public_water_supply>();
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
                    dict[year] = new Public_water_supply { Year = year };
                }

                var row = dict[year];

                switch (sectorCode)
                {
                    case "1": row.Total = value; break;
                    case "2": row.Abstracted_water = value; break;
                    case "3": row.Ground_water = value; break;
                    case "4": row.Springs = value; break;
                    case "5": row.Watercourse = value; break;
                    case "6": row.Reservoir = value; break;
                    case "7": row.Lake = value; break;
                    case "8": row.Water_taken_from_other_water_supply_systems = value; break;
                }
            }
            return dict.Values.ToList();
        }
}
}
