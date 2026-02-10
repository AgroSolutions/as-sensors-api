using as_sensors_application.DTO;
using as_sensors_domain.Entities;


namespace as_sensors_application.Services.Interfaces
{
    public interface ISensorDataService
    {
        Task<SensorDataDTO> GetSensorDataAsync();
        Task<SensorDataDTO> AddSensorDataAsync(SensorDataDTO dto);

    }
}
