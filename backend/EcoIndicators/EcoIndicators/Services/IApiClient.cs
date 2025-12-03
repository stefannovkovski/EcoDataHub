using EcoIndicators.Models;

namespace EcoIndicators.Services
{
    public interface IApiClient
    {
        Task<ApiResponse> FetchData(string url,string query);
    }
}
