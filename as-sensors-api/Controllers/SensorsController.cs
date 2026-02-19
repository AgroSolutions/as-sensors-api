using as_sensors_application.DTO;
using as_sensors_application.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace as_sensors_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorsController : ApiBaseController
    {
        private readonly SensorService _service;

        public SensorsController(SensorService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> SaveSensor([FromBody] Guid fieldId, CancellationToken ct)
        {
            var created = await _service.AddSensorAsync(fieldId, ct);
            return Ok(created);
        }

        [HttpGet]
        public async Task<ActionResult<List<SensorDTOResponse>>> GetAll(CancellationToken ct)
        {
            var sensors = await _service.GetAllSensorsAsync(ct);
            return Ok(sensors);
        }

        [HttpGet("{fieldId:guid}")]
        public async Task<IActionResult> GetSensor([FromRoute] Guid fieldId, CancellationToken ct)
        {
            var items = await _service.GetSensorByFieldId(fieldId, ct);
            return Ok(items);
        }

        /* [HttpPut]
         public async Task<IActionResult> UpdateSensor()
         {
             return Success("Carrinho encontrado/criado com sucesso.");
         }*/

        /*[HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var deleted = await _service.DeleteSensorAsync(id, ct);
            if (!deleted) return NotFound();

            return NoContent();
        }*/
    }
}
