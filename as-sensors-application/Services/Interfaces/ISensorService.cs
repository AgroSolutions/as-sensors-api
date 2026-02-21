using as_sensors_application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace as_sensors_application.Services.Interfaces
{
    public interface ISensorService
    {
        Task<SensorDTOResponse> AddSensorAsync(SensorDTOResquest dto, CancellationToken ct = default);
        Task<List<SensorDTOResponse>> GetAllSensorsAsync(CancellationToken ct = default);
        Task<List<SensorDTOResponse>> GetSensorByFieldId(Guid fieldId, CancellationToken ct = default);
        Task<bool> DeleteSensorAsync(Guid id, CancellationToken ct = default);


    }
}
