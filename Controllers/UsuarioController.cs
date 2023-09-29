using conocelos_v3.Data;
using conocelos_v3.DTOS;
using conocelos_v3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace conocelos_v3.Controllers
{
    [ApiController]
    [Route("api/usuario/")]
    public class UsuarioController : Controller
    {
        #region Contexto y datos de la BD
        private readonly ConocelosV2Context _context;
        public UsuarioController(ConocelosV2Context context)
        {
            _context = context;
        }
        #endregion

        #region Funcion para obtener a todos los usuarios
        [HttpGet("obtener_usuarios")]
        public IActionResult ObtenerTodosUsuarios()
        {
            List<UsuarioDTO> usuariosFullResult = new List<UsuarioDTO>();
            try
            {
                usuariosFullResult = (from usuario in _context.Usuarios 
                                        join rol in _context.Rols
                                        on usuario.RolId equals rol.RolId
                                        select new UsuarioDTO () 
                                        {
                                            UsuarioId = usuario.UsuarioId,
                                            RolId = usuario.RolId,
                                            Email = usuario.Email,
                                            Password = usuario.Password,
                                            Nombre = usuario.Nombre,
                                            Apellidos = usuario.Apellidos,
                                            Estatus = usuario.Estatus == 1UL,
                                            Rol =rol.Nombre
                                        }).ToList();             

                return Ok(usuariosFullResult);
            }
            catch (Exception ex)
            {
                // return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = "error" });
                return StatusCode(StatusCodes.Status500InternalServerError, new { res = ex});
            }
        }
        #endregion

        #region Regresa al usuario por su ID
        /// <summary>
        /// Optiene solo un candidato que corresponde a su ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        // [Authorize]
        [HttpGet("obtener_usuario/{id}")]
        public IActionResult ObtenerCandidato(int id)
        {
            Usuario usuarioResult = _context.Usuarios.Find(id);

            Rol rolUsuarioId = _context.Rols.Find(usuarioResult.RolId);

            usuarioResult.Rol = rolUsuarioId;

            if (usuarioResult == null)
            {
                return BadRequest();
            }
            try
            {
                return Ok(new {usuario = usuarioResult});
            }
            catch (Exception ex)
            {
                // return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = "error" });
                // return StatusCode(StatusCodes.Status500InternalServerError);
                return Ok(ex);
            }
        }
        #endregion


        #region Funcion que agrega un usuario
        /// <summary>
        /// La funcion agrega a un nuevo candidato 
        /// </summary>
        /// <param name="usuaurioNew"></param>
        /// <returns></returns>
        // POST api/productos
        // [Authorize]
        [HttpPost("agregar_usuario")]
        public IActionResult AgregarUsuario([FromBody] UsuarioDTO dto)
        {
            if(dto == null)
            {
                return BadRequest();
            }
            try
            {
                _context.Usuarios.Add(new Usuario() 
                { 
                    RolId = dto.RolId,
                    Email = dto.Email,
                    Password = dto.Password,
                    Estatus = dto.Estatus ? 1UL : 0UL,
                    Nombre = dto.Nombre,
                    Apellidos = dto.Apellidos,

                });
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { res = ex});
            }
        }
        #endregion

        #region Edita los datos de un usuario
        /// <summary>
        /// Edita los datos de un candidato recibiendo como parametro un objeto de tipo candidato
        /// </summary>
        /// <param name="candidateEdit"></param>
        /// <returns></returns>
        /// 
        // [Authorize]
        [HttpPut("editar_usuario")]
        public IActionResult EditarUsuario([FromBody] UsuarioDTO dto)
        {
            Usuario usuario = _context.Usuarios.Find(dto.UsuarioId);

            if (usuario == null)
            {
                return BadRequest();
            }
            try
            {
                usuario.RolId = dto.RolId;
                usuario.Email = dto.Email;
                usuario.Password = dto.Password;
                usuario.Estatus = dto.Estatus ? 1UL : 0UL;
                usuario.Nombre = dto.Nombre;
                usuario.Apellidos = dto.Apellidos;

                _context.Usuarios.Update(usuario);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                // return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, reponse = "error" });
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region Funcion que elimina a un usuario
        /// <summary>
        /// Elimina a un candidato recibiniendo como parametro su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        //[Authorize]
        [HttpDelete("eliminar_usuario/{id:int}")]
        public IActionResult EliminarUsuario(int id)
        {
            Usuario usuarioDelete = _context.Usuarios.Find(id);

            if (usuarioDelete == null)
            {
                return BadRequest();
            }
            try
            {
                _context.Usuarios.Remove(usuarioDelete);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        #endregion
    }
}
