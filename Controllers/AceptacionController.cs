using conocelos_v3.Data;
using conocelos_v3.DTOS;
using conocelos_v3.Models;
using Microsoft.AspNetCore.Mvc;

namespace conocelos_v3.Controllers
{
        [ApiController]
        [Route("api/aceptacion/")]
        public class AceptacionController : Controller
        {
            #region Contexto y datos de la BD
            private readonly ConocelosV2Context _context;
            public AceptacionController(ConocelosV2Context context)
            {
                _context = context;
            }
            #endregion

            #region Funcion para obtener 
            [HttpGet("obtener_aceptacion")]
            public IActionResult ObtenerTodosAceptacion()
            {
                List<AceptacionDTO> aceptacionFullResult = new List<AceptacionDTO>();
                try
                {
                aceptacionFullResult = (from aceptacion in _context.Aceptacion
                                          select new AceptacionDTO()
                                          {
                                              Id = aceptacion.Id,
                                              NombreC = aceptacion.NombreC,
                                              Nombre = aceptacion.Nombre,
                                              Estado = aceptacion.Estado,
                                              Fechadenvio = aceptacion.Fechadenvio,
                                              Fechaaceptacion = aceptacion.Fechaaceptacion
                                          }).ToList();

                    return Ok(aceptacionFullResult);
                }
                catch (Exception ex)
                {
                    // return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = "error" });
                    return StatusCode(StatusCodes.Status500InternalServerError, new { res = ex });
                }
            }
        #endregion
        #region Funcion que agrega 
        [HttpPost("agregar_aceptacion")]
        public IActionResult AgregarAceptacion([FromBody] AceptacionDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            try
            {
                _context.Aceptacion.Add(new Aceptacion()
                {
                    Id = dto.Id,
                    NombreC = dto.NombreC,
                    Nombre = dto.Nombre,
                    Estado = dto.Estado,
                    Fechadenvio = dto.Fechadenvio,
                    Fechaaceptacion = dto.Fechaaceptacion
                });
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { res = ex });
            }
        }
        #endregion
        #region Funcion que elimina 
        [HttpDelete("eliminar_aceptacion/{id:int}")]
        public IActionResult EliminarAceptacion(int id)
        {
            Aceptacion aceptacionDelete = _context.Aceptacion.Find(id);

            if (aceptacionDelete == null)
            {
                return BadRequest();
            }
            try
            {
                _context.Aceptacion.Remove(aceptacionDelete);
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
