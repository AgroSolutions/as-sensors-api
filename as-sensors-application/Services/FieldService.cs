using as_sensors_application.DTO;
using as_sensors_application.Publishers.Interfaces;
using as_sensors_domain.Enum;
using as_sensors_infra.Persistance.Repository.Interfaces;

namespace as_sensors_application.Services
{
    public class FieldService(
        IFieldServicePublisher fieldServicePublisher,
        ISensorDataRepository sensorDataRepository,
        ISensorRepository sensorRepository
        )
    {

        public async Task CalculateFieldStatus(Guid sensorId, SensorDataDTO newReading, CancellationToken ct = default)
        {
            var fieldId = await sensorRepository.GetFieldIdBySensorIdAsync(sensorId);

            if (fieldId == Guid.Empty)
            {
                return;
            }
            else
            {

                var last24h = await sensorDataRepository.GetLast24HoursBySensorIdAsync(sensorId, ct);

                var soilValues = last24h.Select(x => x.SoilMoisturePercentage).ToList();
                var tempValues = last24h.Select(x => x.TemperatureC).ToList();
                var precipValues = last24h.Select(x => x.PrecipitationLevelPercentage).ToList();

                soilValues.Add(newReading.SoilMoisturePercentage);
                tempValues.Add(newReading.TemperatureC);
                precipValues.Add(newReading.PrecipitationLevelPercentage);

                var avgSoil = soilValues.Any() ? soilValues.Average() : 0;
                var avgTemp = tempValues.Any() ? tempValues.Average() : 0;
                var avgPrecip = precipValues.Any() ? precipValues.Average() : 0;

                var status = DetermineStatus(avgSoil, avgTemp, avgPrecip);

                await UpdateFieldStatus(fieldId, status);
            }
        }

        private static FieldStatus DetermineStatus(double soil, double temp, double precip)
        {
            // Pest Risk: temp > 25 e precip > 75
            if (temp > 25 && precip > 75)
                return FieldStatus.PestRisk;

            // Drought alert: soil < 15 e precip <= 75
            if (soil < 15 && precip <= 75)
                return FieldStatus.DroughtAlert;

            return FieldStatus.Normal;
        }

        public async Task UpdateFieldStatus(Guid fieldId, FieldStatus status)
        {
            var dto = new UpdateFieldStatusDTO
            {
                FieldId = fieldId,
                Status = status,
                UpdatedAt = DateTime.UtcNow,
            };

            await fieldServicePublisher.UpdateFieldStatus(dto);
        }
    }
}
