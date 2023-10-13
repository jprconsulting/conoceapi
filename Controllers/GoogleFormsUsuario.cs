using Microsoft.AspNetCore.Mvc;
using conocelos_v3.Data;
using conocelos_v3.DTOS;
using conocelos_v3.Models;

namespace conocelos_v3.Controllers
{
    [Route("api/formulario-usuario")]
    [ApiController]
    public class GoogleFormsUsuario : Controller
    {
        private readonly ConocelosV2Context _context;
        public GoogleFormsUsuario(ConocelosV2Context context)
        {
            _context = context;
        }

        [HttpGet("get-formulario-usuario")]
        public IActionResult GetConfigFormularioUsuarios()
        {
            try
            {
                var formulariosFullResult = _context.GoogleFormUsuarios.Select(g => new GoogleFormUsuarioDTO()
                {
                    FormularioId = g.FormularioId,
                    UsuarioId = g.UsuarioId,
                }).ToList();

                return Ok(formulariosFullResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("post-formulario-usuario")]
        public IActionResult PostConfigFormularioUsuarios([FromBody] GoogleFormUsuarioDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            try
            {
                _context.GoogleFormUsuarios.Add(new GoogleFormUsuario()
                {
                    FormularioId = dto.FormularioId,
                    UsuarioId = dto.UsuarioId,
    
                });

                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { res = ex });
            }
        }
    }
}
