using EcoIndicators.Data;
using EcoIndicators.Services;
using EcoIndicators.Services.MakStat.MakStat;
using EcoIndicators.Services.MakStat;
using Microsoft.EntityFrameworkCore;
using EcoIndicators.Services.MakStat.Indicators.CO2;
using EcoIndicators.Services.MakStat.Indicators.CO2.Loaders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient<IApiClient, ApiClient>();
builder.Services.AddScoped<IMakStatService, MakStatService>();
builder.Services.AddScoped<ICo2Service, Co2Service>();
builder.Services.AddScoped<ICo2BySectorLoader, Co2BySectorLoader>();
builder.Services.AddScoped<ITotalEmissionCO2Loader, TotalEmissionCO2Loader>();
builder.Services.AddScoped<ITotalEmissionSO2Loader,TotalEmissionSO2Loader>();


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