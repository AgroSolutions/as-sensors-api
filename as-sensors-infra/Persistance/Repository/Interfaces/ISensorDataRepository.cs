using as_sensors_domain.Entities;

namespace as_sensors_infra.Persistance.Repository.Interfaces
{
    public interface ISensorDataRepository
    {
        Task<SensorData> InsertAsync(SensorData sensorData, CancellationToken ct = default);
        Task<List<SensorData>> GetBySensorIdAsync(Guid sensorId, CancellationToken ct = default);
    }
}
