using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace AS.Sensors.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SensorsController : ApiBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            return Success("Carrinho encontrado/criado com sucesso.");
        }
    }
}
