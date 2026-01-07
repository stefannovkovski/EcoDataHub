namespace EcoIndicators.Services.MakStat
{
    public interface IMakStatService
    {
        Task<object?> GetTableAsync(string table, int fromYear, int toYear);

        Task LoadData();
    }
}
