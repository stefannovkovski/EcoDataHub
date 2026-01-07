using EcoIndicators.Data;
using EcoIndicators.Services;
using EcoIndicators.Services.MakStat.MakStat;
using EcoIndicators.Services.MakStat;
using EcoIndicators.Services.PulseEco;
using Microsoft.EntityFrameworkCore;
using EcoIndicators.Services.MakStat.Indicators.CO2;
using EcoIndicators.Services.MakStat.Indicators.CO2.Loaders;
using EcoIndicators.Services.MakStat.Indicators.Water;
using EcoIndicators.Services.MakStat.Indicators.Water.Loaders;
using System.Text;
using EcoIndicators.Services.MakStat.Indicators.Waste;
using EcoIndicators.Services.MakStat.Indicators.Waste.Loaders;
using EcoIndicators.Services.MakStat.Indicators.Water.Queries;
using EcoIndicators.Services.MakStat.Indicators.Waste.Queries;
using EcoIndicators.Services.MakStat.Indicators.CO2_.Queries;

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

//Pulse Eco Services
builder.Services.AddHttpClient<IPulseEcoService, PulseEcoService>();


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


builder.Services.AddScoped<IWasteWaterService, WasterWaterService>();
builder.Services.AddScoped<IPublic_water_supply_Service, Public_water_supply_Service>();
builder.Services.AddScoped<IWater_abstracted_by_bussiness_Service, Water_abstracted_by_bussiness_Service>();
builder.Services.AddScoped<IWaterForProductionPurposesService, WaterForProductionPurposesService>();
builder.Services.AddScoped<IWaterBusinessPurposesService, WaterBusinessPurposesService>();

builder.Services.AddScoped<IAmountMunicipalWasteService, AmountMunicipalWasteService>();
builder.Services.AddScoped<IWaste_by_site_of_generations_Service, Waste_by_site_of_generation_Service>();
builder.Services.AddScoped<ICollectedAndGeneratedWasteService, CollectedAndGeneratedWasteService>();

builder.Services.AddScoped<ITotalEmissionCO2Service, TotalEmissionCO2Service>();
builder.Services.AddScoped<ITotalEmissionsSO2Service, TotalEmissionSO2Service>();
builder.Services.AddScoped<ICo2BySectorService, Co2BySectorService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => {
    options.AddPolicy("ReactPolicy", policy => {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("ReactPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();