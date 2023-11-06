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

        [HttpGet("get-formulario-usuario/{formularioId}")]
        public IActionResult GetUsuariosAsignadosAFormulario(int formularioId)
        {
            try
            {
                var asignaciones = _context.GoogleFormUsuarios
                    .Where(g => g.FormularioId == formularioId)
                    .ToList();

                var usuariosAsignados = asignaciones.Select(g => new UsuarioIdDTO
                {
                    UsuarioId = g.UsuarioId,
                    Nombre = _context.Usuarios.FirstOrDefault(u => u.UsuarioId == g.UsuarioId)?.Nombre
                }).ToList();

                return Ok(usuariosAsignados);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { res = ex });
            }
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

        [HttpPost("editar_usuario")]
        public IActionResult EditarUsuarios([FromBody] GoogleFormUsuarioDTO dto)
        {
            try
            {
                int formularioId = dto.FormularioId;

                if (formularioId <= 0)
                {
                    return BadRequest("El formularioId no se proporcionó correctamente en el DTO.");
                }

                var registrosAEliminar = _context.GoogleFormUsuarios.Where(g => g.FormularioId == formularioId);
                _context.GoogleFormUsuarios.RemoveRange(registrosAEliminar);

                foreach (var usuarioId in dto.UsuarioIds)
                {
                    _context.GoogleFormUsuarios.Add(new GoogleFormUsuario
                    {
                        FormularioId = formularioId,
                        UsuarioId = usuarioId,
                    });
                }

                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message, response = "error" });
            }
        }

        [HttpGet("get-formulario-usuarios/{formularioId}")]
        public IActionResult GetConfigFormularioUsuarios(int formularioId)
        {
            try
            {
                var asignaciones = _context.GoogleFormUsuarios
                    .Where(g => g.FormularioId == formularioId)
                    .ToList();

                var formulariosConUsuarios = asignaciones.Select(g => new GoogleFormUsuarioDTO
                {
                    FormularioUsuarioId = g.FormularioUsuarioId,
                    FormularioId = g.FormularioId,
                    UsuarioIds = new List<int> { g.UsuarioId }
                }).ToList();

                return Ok(formulariosConUsuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { res = ex });
            }
        }


    }
}
