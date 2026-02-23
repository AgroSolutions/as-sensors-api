using as_sensors_application.Observability;
using as_sensors_application.Publishers.Interfaces;

namespace as_sensors_application.Services
{
    public class FieldService(IHttpClientFactory httpClientFactory, IFieldServicePublisher fieldServicePublisher, ISensorTelemetry telemetry)
    {
        public Task CalculateFieldStatus()
        {
            return Task.CompletedTask;
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
