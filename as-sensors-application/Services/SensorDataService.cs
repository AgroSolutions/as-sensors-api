using as_sensors_application.DTO;
using as_sensors_application.Services.Interfaces;
using as_sensors_domain.Entities;
using as_sensors_infra.Persistance.Repository.Interfaces;

namespace as_sensors_application.Services
{
    public class SensorDataService : ISensorDataService
    {
        private readonly ISensorDataRepository _repository;

        public SensorDataService(ISensorDataRepository repository)
        {
            _repository = repository;
        }
        public async Task<SensorDataDTOResponse> AddSensorDataAsync(SensorDataDTO dto, CancellationToken ct = default)
        {
            var entity = new SensorData
            {
                SensorId = dto.SensorId,
                Date = DateTime.UtcNow,
                SoilMoisturePercentage = dto.SoilMoisturePercentage,
                TemperatureC = dto.TemperatureC,
                PrecipitationLevelPercentage = dto.PrecipitationLevelPercentage,
            };

            var created = await _repository.InsertAsync(entity, ct);

            return new SensorDataDTOResponse
            {
                Id = created.Id,
                SensorId = created.SensorId,
                Date = created.Date,
                SoilMoisturePercentage = created.SoilMoisturePercentage,
                TemperatureC = created.TemperatureC,
                PrecipitationLevelPercentage = created.PrecipitationLevelPercentage
            };

        }

        public async Task<List<SensorDataDTOResponse>> GetSensorDataBySensorIdAsync(Guid sensorId, CancellationToken ct = default)
        {
            var items = await _repository.GetBySensorIdAsync(sensorId, ct);

            return items.Select(x => new SensorDataDTOResponse
            {
                Id = x.Id,
                SensorId = x.SensorId,
                Date = x.Date,
                SoilMoisturePercentage = x.SoilMoisturePercentage,
                TemperatureC = x.TemperatureC,
                PrecipitationLevelPercentage = x.PrecipitationLevelPercentage
            }).ToList();
        }
    }
}
