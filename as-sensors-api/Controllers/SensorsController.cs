using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace as_sensors_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorsController : ApiBaseController
    {
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
    }
}
