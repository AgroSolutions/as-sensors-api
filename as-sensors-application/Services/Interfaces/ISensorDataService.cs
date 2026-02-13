using as_sensors_application.DTO;
using as_sensors_domain.Entities;


namespace as_sensors_application.Services.Interfaces
{
    public interface ISensorDataService
    {
        Task<List<SensorDataDTOResponse>> GetSensorDataBySensorIdAsync(Guid sensorId, CancellationToken ct = default);
        Task<SensorDataDTOResponse> AddSensorDataAsync(SensorDataDTO dto, CancellationToken ct = default);
    }
}
