using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace as_sensors_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ApiBaseController
    {
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