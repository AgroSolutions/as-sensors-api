using as_sensors_domain.Entities;
using as_sensors_infra.Persistance.Config;
using as_sensors_infra.Persistance.Repository.Interfaces;
using MongoDB.Driver;
using System.Drawing;

namespace as_sensors_infra.Persistance.Repository
{
    public class SensorDataRepository : ISensorDataRepository
    {
        private IMongoCollection<SensorData> _sensorData;

        public SensorDataRepository(MongoContext ctx)
        {
            _sensorData = ctx.Database.GetCollection<SensorData>("sensors_data");
        }

        public async Task<SensorData?> GetByIdAsync(int id, CancellationToken ct = default)
            => await _sensorData.Find(x => x.Id == id).FirstOrDefaultAsync(ct);

        public async Task<SensorData> InsertAsync(SensorData sensorData, CancellationToken ct = default)
        {
            await _sensorData.InsertOneAsync(sensorData, cancellationToken: ct);
            return sensorData;
        }
    }
}
