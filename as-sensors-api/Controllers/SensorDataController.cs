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

        [HttpGet]
        public async Task<IActionResult> GetSensorData()
        {
            var items = await _service.GetSensorDataAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> SaveSensorData([FromBody] SensorDataDTO dto)
        {
            var created = await _service.AddSensorDataAsync(dto);
            return Ok(created);

        }
    }
}
