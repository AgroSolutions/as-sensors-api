using as_sensors_application.DTO;
using as_sensors_application.Services.Interfaces;
using as_sensors_infra.Persistance.Repository.Interfaces;

namespace as_sensors_application.Services
{
    public class SensorDataService(
        ISensorDataRepository sensorDataRepository
    ) : ISensorDataService
    {
        public async Task<SensorDataDTO> AddSensorDataAsync(SensorDataDTO dto)
        {
            await sensorDataRepository.AddAsync(dto);
        }

        public Task<SensorDataDTO> GetSensorDataAsync()
        {
            throw new NotImplementedException();
        }
    }
}
