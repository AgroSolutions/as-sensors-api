using as_sensors_application.DTO;
using as_sensors_application.Publishers.Interfaces;
using as_sensors_domain.Messaging.Interfaces;
using as_sensors_infra.Messaging.Config;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace as_sensors_application.Publishers
{
    public class FieldServicePublisher(
        IQueuePublisher publisher,
        IOptions<ExchangesOptions> exchanges,
        IOptions<QueuesOptions> queues
    ) : IFieldServicePublisher
    {
        public Task UpdateFieldStatus(UpdateFieldStatusDTO dto)
        {
            return publisher.PublishAsync(
                dto,
                queues.Value.UpdateFieldStatusQueue,
                exchanges.Value.FieldExchange
            );
        }
    }
}
