using as_sensors_application.DTO;
using as_sensors_application.Observability;
using as_sensors_application.Publishers.Interfaces;
using as_sensors_infra.Persistance.Repository.Interfaces;

namespace as_sensors_application.Services
{
    public class FieldService(
        IHttpClientFactory httpClientFactory, 
        IFieldServicePublisher fieldServicePublisher,
        ISensorDataRepository sensorDataRepository,
        ISensorRepository sensorRepository,
        ISensorTelemetry telemetry
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

        private static string DetermineStatus(double soil, double temp, double precip)
        {
            // Pest Risk: temp > 25 e precip > 75
            if (temp > 25 && precip > 75)
                return "PestRisk";

            // Drought alert: soil < 15 e precip <= 75
            if (soil < 15 && precip <= 75)
                return "DroughtAlert";

            // Normal
            if (soil <= 15 && temp <= 25 && precip <= 75)
                return "Normal";

            return "Unknown";
        }

        public async Task UpdateFieldStatus(Guid _fieldId, string _status)
        {
            var dto = new DTO.UpdateFieldStatusDTO
            {
                FieldId = _fieldId,
                Status = _status,
                UpdatedAt = DateTime.UtcNow,
            };

            try
            {
                await fieldServicePublisher.UpdateFieldStatus(dto);
                telemetry.FieldStatusUpdatePublished(_status, success: true);
            }
            catch (Exception ex)
            {
                telemetry.FieldStatusUpdatePublished(_status, success: false, failureReaon: ex.GetType().Name);
                throw;
            }
        }
    }
}
