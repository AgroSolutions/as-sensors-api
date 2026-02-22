using as_sensors_application.Publishers.Interfaces;

namespace as_sensors_application.Services
{
    public class FieldService(IHttpClientFactory httpClientFactory, IFieldServicePublisher fieldServicePublisher)
    {
        public Task calculateFieldStatus()
        {
            return Task.CompletedTask;
        }
        public Task UpdateFieldStatus(Guid _fieldId, string _status)
        {
            var dto = new DTO.UpdateFieldStatusDTO
            {
                fieldId = _fieldId,
                status = _status,
                date = DateTime.UtcNow,
            };
            return fieldServicePublisher.UpdateFieldStatus(dto);
        }
    }
}
