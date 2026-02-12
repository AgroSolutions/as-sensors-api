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
                Id = 1,
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

        public Task<SensorDataDTOResponse> GetSensorDataAsync()
        {
            throw new NotImplementedException();
        }
    }
}
