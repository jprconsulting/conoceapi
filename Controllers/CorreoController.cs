using conocelos_v3.Data;
using conocelos_v3.DTOS;
using conocelos_v3.Models;
using Microsoft.AspNetCore.Mvc;

namespace conocelos_v3.Controllers
{
    [ApiController]
    [Route("api/correo/")]
    public class CorreoController : Controller
    {
        #region Contexto y datos de la BD
        private readonly ConocelosV2Context _context;
        public CorreoController(ConocelosV2Context context)
        {
            _context = context;
        }
        #endregion

        #region Funcion para obtener a todos los correo
        [HttpGet("obtener_correo")]
        public IActionResult ObtenerTodosCorreo()
        {
            List<CorreoDTO> correoFullResult = new List<CorreoDTO>();
            try
            {
                correoFullResult = (from correo in _context.Correo
                                     select new CorreoDTO()
                                     {
                                         Id = correo.Id,
                                         EmailOrigen = correo.EmailOrigen,
                                         Contraseña = correo.Contraseña,
                                         Credenciales = correo.Credenciales,
                                         NombreUsuario = correo.NombreUsuario,
                                         ServidorOrigen = correo.ServidorOrigen,
                                         PuertoOrigen = correo.PuertoOrigen,
                                         ConfiarCertificado = correo.ConfiarCertificado
                                     }).ToList();

               
                return Ok(correoFullResult);
            }
            catch (Exception ex)
            {
                // return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = "error" });
                return StatusCode(StatusCodes.Status500InternalServerError, new { res = ex });
            }
        }
        #endregion
        
    }
}
