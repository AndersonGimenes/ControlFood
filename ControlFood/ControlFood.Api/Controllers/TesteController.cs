using Microsoft.AspNetCore.Mvc;

namespace ControlFood.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TesteController : ControllerBase
    {
        [HttpGet]
        public IActionResult Obter()
        {
            return Ok("Teste");
        }

        [HttpPost("Inserir")]
        public IActionResult Inserir()
        {
            return Ok("Teste");
        }
    }
}
