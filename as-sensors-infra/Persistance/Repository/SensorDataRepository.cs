using as_sensors_domain.Entities;
using as_sensors_infra.Persistance.Repository.Interfaces;
using MongoDB.Driver;

namespace as_sensors_infra.Persistance.Repository
{
    public class SensorDataRepository(IMongoDatabase database) : ISensorDataRepository
    {
        private IMongoCollection<SensorData> _sensorData = database.GetCollection<SensorData>("SensorData");
        public async Task AddAsync(SensorData sensorData)
        {
            await _sensorData.InsertOneAsync(sensorData);
        }
    }
}
