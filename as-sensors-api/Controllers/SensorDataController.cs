using as_sensors_application.DTO;
using as_sensors_application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace as_sensors_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorDataController : ApiBaseController
    {
        private readonly SensorDataService _service;

        public SensorDataController(SensorDataService service)
        {
            _service = service;
        }

        [HttpGet("{sensorId:guid}")]
        public async Task<IActionResult> GetSensorData([FromRoute] Guid sensorId, CancellationToken ct)
        {
            var items = await _service.GetSensorDataBySensorIdAsync(sensorId, ct);
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> SaveSensorData([FromBody] SensorDataDTO dto, CancellationToken ct)
        {
            var created = await _service.AddSensorDataAsync(dto, ct);
            return Ok(created);
        }
    }
}
