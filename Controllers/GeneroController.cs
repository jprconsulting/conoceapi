using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using conocelos_v3.Data;
using conocelos_v3.DTOS;
using conocelos_v3.Models;

namespace conocelos_v3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly ConocelosV2Context _context;
        public GeneroController(ConocelosV2Context context)
        {
            _context = context;
        }

        [HttpGet("obtener_usuarios")]
        public IActionResult ObtenerTodosUsuarios()
        {
            List<GeneroDTO> usuariosFullResult = new List<GeneroDTO>();
            try
            {
                usuariosFullResult = (from genero in _context.Generos
                                      
                                      select new GeneroDTO()
                                      {
                                          GeneroId = genero.GeneroId,
                                          NombreGenero = genero.NombreGenero,
                                      }).ToList();

                return Ok(usuariosFullResult);
            }
            catch (Exception ex)
            {
                // return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = "error" });
                return StatusCode(StatusCodes.Status500InternalServerError, new { res = ex });
            }
        }
    }
}
