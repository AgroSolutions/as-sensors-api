
using as_sensors_application.DTO;
using as_sensors_domain.Messaging.Interfaces;
using as_sensors_infra.Messaging.Config;
using Microsoft.Extensions.Options;

namespace as_sensors_api
{
    public class WorkerCreateSensor : BackgroundService
    {
        private readonly IQueueConsumer _consumer;
        private readonly IMessageHandler<SensorDTOResquest> _handler;
        private readonly QueuesOptions _queues;

        public WorkerCreateSensor(
            IQueueConsumer consumer,
            IMessageHandler<SensorDTOResquest> handler,
            IOptions<QueuesOptions> queuesOptions
            ) 
        { 
            _consumer = consumer;
            _handler = handler;
            _queues = queuesOptions.Value;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumer.StartAsync(
                _queues.CreateSensorQueue,
                _handler,
                stoppingToken);
        }
    }
}
