using conocelos_v3.Data;
using conocelos_v3.DTOS;
using conocelos_v3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace conocelos_v3.Controllers
{
        [ApiController]
        [Route("api/personalizacion/")]
        public class PersonalizacionController : Controller
        {
            #region Contexto y datos de la BD
            private readonly ConocelosV2Context _context;
            public PersonalizacionController(ConocelosV2Context context)
            {
                _context = context;
            }
        #endregion
        #region Funcion para obtener personalizacion
        [HttpGet("obtener_personalizacion")]
        public IActionResult ObtenerPersonalizacion()
        {
            List<PersonalizacionDTO> personalizacionFullResult = new List<PersonalizacionDTO>();
            try
            {
                personalizacionFullResult = (from personalizacion in _context.Personalizacion
                                               select new PersonalizacionDTO()
                                      {
                                          PersonalizacionId = personalizacion.PersonalizacionId,
                                          LogoIntitucional = personalizacion.LogoIntitucional,
                                          LogoAplicacion = personalizacion.LogoAplicacion,
                                          ImagenBienvenida = personalizacion.ImagenBienvenida,
                                          NumeroTelefono = personalizacion.NumeroTelefono,
                                          Direccion = personalizacion.Direccion,
                                          URLFacebook = personalizacion.URLFacebook,
                                          URLInstagram = personalizacion.URLInstagram,
                                          URLTwitter = personalizacion.URLTwitter,
                                          URLYoutube = personalizacion.URLYoutube,
                                          Colorprimario = personalizacion.Colorprimario,
                                          Colorsecundario = personalizacion.Colorsecundario
                                      }).ToList();

                return Ok(personalizacionFullResult);
            }
            catch (Exception ex)
            {
                // return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = "error" });
                return StatusCode(StatusCodes.Status500InternalServerError, new { res = ex });
            }
        }
        #endregion
        #region Funcion que agrega personalizacion
        [HttpPost("agregar_personalizacion")]
        public IActionResult AgregarPersonalizacion([FromBody] PersonalizacionDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            try
            {
                // Verificar si ya existe algún registro en la tabla
                var cantidadRegistros = _context.Personalizacion.Count();

                if (cantidadRegistros > 0)
                {
                    var errorResponse = new
                    {
                        mensaje = "Ya existe un registro de personalización. No se puede agregar otro.",
                        error = "Conflicto"
                    };

                    return Conflict(errorResponse);
                }

                // Agregar el nuevo registro de personalización
                _context.Personalizacion.Add(new Personalizacion()
                {
                    PersonalizacionId = dto.PersonalizacionId,
                    LogoIntitucional = dto.LogoIntitucional,
                    LogoAplicacion = dto.LogoAplicacion,
                    ImagenBienvenida = dto.ImagenBienvenida,
                    NumeroTelefono = dto.NumeroTelefono,
                    Direccion = dto.Direccion,
                    URLFacebook = dto.URLFacebook,
                    URLInstagram = dto.URLInstagram,
                    URLTwitter = dto.URLTwitter,
                    URLYoutube = dto.URLYoutube,
                    Colorprimario = dto.Colorprimario,
                    Colorsecundario = dto.Colorsecundario
                });

                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                // Manejar otros errores y devolver una respuesta personalizada
                var errorResponse = new
                {
                    mensaje = "Ocurrió un error al agregar la personalización.",
                    error = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }
        #endregion

        #region Edita los datos de un Personalizacion
        [HttpPut("editar_personalizacion")]
        public IActionResult Editarpersonalizacion([FromBody] PersonalizacionDTO dto)
        {
            Personalizacion personalizacion = _context.Personalizacion.Find(dto.PersonalizacionId);

            if (personalizacion == null)
            {
                return BadRequest();
            }
            try
            {
                personalizacion.PersonalizacionId = dto.PersonalizacionId;
                personalizacion.LogoIntitucional = dto.LogoIntitucional;
                personalizacion.LogoAplicacion = dto.LogoAplicacion;
                personalizacion.ImagenBienvenida = dto.ImagenBienvenida;
                personalizacion.NumeroTelefono = dto.NumeroTelefono;
                personalizacion.Direccion = dto.Direccion;
                personalizacion.URLFacebook = dto.URLFacebook;
                personalizacion.URLInstagram = dto.URLInstagram;
                personalizacion.URLTwitter = dto.URLTwitter;
                personalizacion.URLYoutube = dto.URLYoutube;
                personalizacion.Colorprimario = dto.Colorprimario;
                personalizacion.Colorsecundario = dto.Colorsecundario;

                _context.Personalizacion.Update(personalizacion);
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
        #region Funcion que elimina 
        [HttpDelete("eliminar_personalizacion/{id:int}")]
        public IActionResult EliminarPersonalizacion(int id)
        {
            Personalizacion personalizacionDelete = _context.Personalizacion.Find(id);

            if (personalizacionDelete == null)
            {
                return BadRequest();
            }
            try
            {
                _context.Personalizacion.Remove(personalizacionDelete);
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
