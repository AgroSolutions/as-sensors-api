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

        public async Task<SensorDTOResponse> AddSensorAsync(SensorDTOResquest dto, CancellationToken ct = default)
        {
            var entity = new Sensor
            {
                Name = dto.Name,
                FieldId = dto.FieldId,
                CreatedAt = DateTime.Now, 
            };

            var created = await _repository.InsertAsync(entity, ct);

            return new SensorDTOResponse
            {
                Id = created.Id,
                Name = created.Name,
                FieldId = created.FieldId,
                CreatedAt = created.CreatedAt,
            };

        }

        public async Task<bool> DeleteSensorAsync(Guid id, CancellationToken ct = default)
        {
            return await _repository.DeleteAsync(id, ct);
        }

        public async Task<List<SensorDTOResponse>> GetAllSensorsAsync(CancellationToken ct = default)
        {
            var sensors = await _repository.GetAllAsync(ct);

            return sensors.Select(s => new SensorDTOResponse
            {
                Id = s.Id,
                Name = s.Name,
                FieldId = s.FieldId,
                CreatedAt = s.CreatedAt,
            }).ToList();
        }

        public async Task<List<SensorDTOResponse>> GetSensorByFieldId(Guid fieldId, CancellationToken ct = default)
        {
            var items = await _repository.GetAllByFieldIdAsync(fieldId, ct);

            return items.Select(x => new SensorDTOResponse
            {
                Id = x.Id,
                Name = x.Name,
                FieldId = x.FieldId,
                CreatedAt = x.CreatedAt,
            }).ToList();
        }
    }
}
