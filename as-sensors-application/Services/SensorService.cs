using as_sensors_application.DTO;
using as_sensors_application.Services.Interfaces;
using as_sensors_domain.Entities;
using as_sensors_infra.Persistance.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace as_sensors_application.Services
{
    public class SensorService : ISensorService
    {
        private readonly ISensorRepository _repository;

        public SensorService(ISensorRepository repository)
        {
            _repository = repository;
        }

        public async Task<SensorDTOResponse> AddSensorAsync(Guid fieldId, CancellationToken ct = default)
        {
            var entity = new Sensor
            {
                FieldId = fieldId,
            };

            var created = await _repository.InsertAsync(entity, ct);

            return new SensorDTOResponse
            {
                Id = created.Id,
                FieldId = created.FieldId,
            };

        }
    }
}
