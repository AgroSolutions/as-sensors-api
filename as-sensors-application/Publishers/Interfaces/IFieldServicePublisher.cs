
using as_sensors_application.DTO;

namespace as_sensors_application.Publishers.Interfaces
{
    public interface IFieldServicePublisher
    {
        Task UpdateFieldStatus(UpdateFieldStatusDTO dto);
    }
}
