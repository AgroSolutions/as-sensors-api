using as_sensors_domain.Entities;
using as_sensors_infra.Persistance.Config;
using as_sensors_infra.Persistance.Repository.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace as_sensors_infra.Persistance.Repository
{
    public class SensorRepository : ISensorRepository
    {
        private IMongoCollection<Sensor> _sensor;

        public SensorRepository(MongoContext ctx)
        {
            _sensor = ctx.Database.GetCollection<Sensor>("sensors");
        }

        public async Task<bool> DeleteAsync(Guid sensorId, CancellationToken ct = default)
        {
            var result = await _sensor.DeleteOneAsync(x => x.Id == sensorId, ct);
            return result.DeletedCount > 0;
        }

        public async Task<List<Sensor>> GetAllAsync(CancellationToken ct = default)
            =>await _sensor
                .Find(FilterDefinition<Sensor>.Empty)
                .ToListAsync(ct);

        public async Task<List<Sensor>> GetAllByFieldIdAsync(Guid fieldId, CancellationToken ct = default)
            =>await _sensor
            .Find(x => x.FieldId == fieldId)
            .ToListAsync(ct);

        public async Task<Sensor> InsertAsync(Sensor sensor, CancellationToken ct = default)
        {
            await _sensor.InsertOneAsync(sensor, cancellationToken: ct);
            return sensor;
        }
    }
}
