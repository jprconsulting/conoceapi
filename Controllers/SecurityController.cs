using conocelos_v3.DTOS;
using conocelos_v3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace conocelos_v3.Controllers
{
    [Route("api/security")]
    [ApiController]
    public class SecurityController : ControllerBase
    {

        private readonly IAutorizacionService _autorizacionService;

        public SecurityController(IAutorizacionService autorizacionService)
        {
            _autorizacionService = autorizacionService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AutorizacionRequestDTO autorizacion)
        {
            try
            {
                var resultado_autorizacion = await _autorizacionService.DevolverToken(autorizacion);
                if (resultado_autorizacion == null)
                    return Unauthorized();

                return Ok(resultado_autorizacion);
            }
             
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message });
            }
        }


    }
}
