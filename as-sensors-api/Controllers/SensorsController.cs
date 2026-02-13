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
        public async Task<IActionResult> GetSensor()
        {
            return Success("Carrinho encontrado/criado com sucesso.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSensor()
        {
            return Success("Carrinho encontrado/criado com sucesso.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSensor()
        {
            return Success("Carrinho encontrado/criado com sucesso.");
        }

        [HttpGet("mongo")]
        public async Task<IActionResult> Mongo()
        {
            var uri = "mongodb+srv://fiap:admin123@cluster0.lzxq3kz.mongodb.net/?appName=Cluster0";
            if (string.IsNullOrWhiteSpace(uri))
                return Problem("MONGODB_URI não configurada.");

            try
            {
                var client = new MongoClient(uri);
                var adminDb = client.GetDatabase("admin");
                await adminDb.RunCommandAsync<BsonDocument>(new BsonDocument("ping", 1));

                return Ok(new { ok = true, message = "Mongo conectado ✅" });
            }
            catch (Exception ex)
            {
                return Problem($"Falha ao conectar no MongoDB: {ex.Message}");
            }
        }
    }
}
