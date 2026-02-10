using as_sensors_domain.Entities;
using as_sensors_application.DTO;

namespace as_sensors_infra.Persistance.Repository.Interfaces
{
    public interface ISensorDataRepository
    {
        Task AddAsync(SensorDataDTO dto);
    }
}
