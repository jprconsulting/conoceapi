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
                var grupos = _context.GoogleFormUsuarios
                    .GroupBy(g => g.FormularioId)
                    .ToList();

                var formulariosConUsuarios = grupos.SelectMany(group => group.Select(g => new GoogleFormUsuarioDTO
                {
                    FormularioUsuarioId = g.FormularioUsuarioId, 
                    FormularioId = group.Key,
                    UsuarioIds = new List<int> { g.UsuarioId }
                }))
                .ToList();

                return Ok(formulariosConUsuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { res = ex });
            }
        }



        [HttpPost("post-formulario-usuario")]
        public IActionResult PostConfigFormularioUsuarios([FromBody] GoogleFormUsuarioDTO dto)
        {
            if (dto == null || dto.UsuarioIds == null || dto.UsuarioIds.Count == 0)
            {
                return BadRequest("La lista de UsuarioId está vacía o nula.");
            }

            try
            {
                foreach (int usuarioId in dto.UsuarioIds)
                {
                    _context.GoogleFormUsuarios.Add(new GoogleFormUsuario()
                    {
                        FormularioId = dto.FormularioId,
                        UsuarioId = usuarioId,
                    });
                }

                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { res = ex });
            }
        }

        [HttpDelete("delete-formulario-usuario")]
        public IActionResult DeleteConfigFormularioUsuario(int formularioId, int usuarioId)
        {
            try
            {
                var formularioUsuario = _context.GoogleFormUsuarios
                    .FirstOrDefault(g => g.FormularioId == formularioId && g.UsuarioId == usuarioId);

                if (formularioUsuario == null)
                {
                    return NotFound(); 
                }

                _context.GoogleFormUsuarios.Remove(formularioUsuario);
                _context.SaveChanges();

                return NoContent(); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { res = ex });
            }
        }

        [HttpPut("update-formulario-usuario")]
        public IActionResult UpdateConfigFormularioUsuario([FromBody] GoogleFormUsuarioDTO dto)
        {
            try
            {
                var formularioUsuario = _context.GoogleFormUsuarios
                    .FirstOrDefault(g => g.FormularioUsuarioId == dto.FormularioUsuarioId);

                if (formularioUsuario == null)
                {
                    return NotFound("El registro no fue encontrado.");
                }

                formularioUsuario.FormularioId = dto.FormularioId;

                _context.SaveChanges();

                return Ok(formularioUsuario); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { res = ex });
            }
        }

    }
}
