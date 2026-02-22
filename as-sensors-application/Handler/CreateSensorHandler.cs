
using as_sensors_application.DTO;
using as_sensors_application.Services.Interfaces;
using as_sensors_domain.Messaging.Interfaces;

namespace as_sensors_application.Handler
{
    public class CreateSensorHandler(ISensorService sensorService) : IMessageHandler<SensorDTOResquest>
    {
        public async Task HandleAsync(SensorDTOResquest message, CancellationToken cancellationToken = default)
        {
            await sensorService.AddSensorAsync(message, cancellationToken);
            Console.WriteLine($"Processando a inserção do sensor");
        }
    }
}
