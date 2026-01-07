    using EcoIndicators.Models.PulseEcoModels;

    namespace EcoIndicators.Services.PulseEco {
        public interface IPulseEcoService {
            Task<EcoPulseSensorDataDto[]> GetCityAverageDataAsync(
                string cityName,
                string valueType,
                DateTime from,
                DateTime to,
                string avgLevel ,
                string sensorId 
            );
        }
    }


