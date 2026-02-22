using as_sensors_application.Publishers.Interfaces;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace as_sensors_application.Services
{
    public class FieldService(IHttpClientFactory httpClientFactory, IFieldServicePublisher fieldServicePublisher)
    {
        public Task UpdateFieldStatus()
        {
            var dto = new DTO.UpdateFieldStatusDTO
            {
                UserId = user.Id,
                GamesId = gamesId
            };
            return fieldServicePublisher.UpdateFieldStatus(dto);
        }
    }
}
