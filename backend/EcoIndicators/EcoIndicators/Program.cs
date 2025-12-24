using EcoIndicators.Data;
using EcoIndicators.Services;
using EcoIndicators.Services.MakStat.MakStat;
using EcoIndicators.Services.MakStat;
using Microsoft.EntityFrameworkCore;
using EcoIndicators.Services.MakStat.Indicators.CO2;
using EcoIndicators.Services.MakStat.Indicators.CO2.Loaders;
using EcoIndicators.Services.MakStat.Indicators.Water;
using EcoIndicators.Services.MakStat.Indicators.Water.Loaders;
using System.Text;
using EcoIndicators.Services.MakStat.Indicators.Waste;
using EcoIndicators.Services.MakStat.Indicators.Waste.Loaders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

//MakStat Services
builder.Services.AddHttpClient<IApiClient, ApiClient>();
builder.Services.AddScoped<IMakStatService, MakStatService>();
builder.Services.AddScoped<ICo2Service, Co2Service>();
builder.Services.AddScoped<ICo2BySectorLoader, Co2BySectorLoader>();
builder.Services.AddScoped<ITotalEmissionCO2Loader, TotalEmissionCO2Loader>();
builder.Services.AddScoped<ITotalEmissionSO2Loader,TotalEmissionSO2Loader>();
<<<<<<< HEAD
=======
builder.Services.AddScoped<PulseEcoService>();

>>>>>>> 0e223e9 (added PulseEco API)

builder.Services.AddScoped<IWaterService, WaterService>();
builder.Services.AddScoped<IWaterForProductionPurposes, WaterForProductionPurposes>();
builder.Services.AddScoped<IWaterBusinessPurpose, WaterBusinessPurpose>();
builder.Services.AddScoped<IPublic_water_supply, Public_water>();
builder.Services.AddScoped<IWater_abstracted_by_business, Water_Abstracted_ByBusiness>();
builder.Services.AddScoped<IWasteWater, WasteWater>();

builder.Services.AddScoped<IWasteService, WasteService>();
builder.Services.AddScoped<IAmountMunicipalWaste, AmountMunicipalWaste>();
builder.Services.AddScoped<IWaste_by_site_of_generation, WasteBySite>();
builder.Services.AddScoped<ICollected_and_generated_municipal_waste, CollectedAndGeneratedWaste>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();