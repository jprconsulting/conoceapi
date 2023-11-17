using conocelos_v3.Data;
using conocelos_v3.DTOS;
using conocelos_v3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                                              idCandidato =aceptacion.idCandidato,
                                              Nombre = aceptacion.Nombre,
                                              Apat = aceptacion.Apat,
                                              Amat = aceptacion.Amat,
                                              Email = aceptacion.Email,
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
                    idCandidato = dto.idCandidato,
                    Nombre = dto.Nombre,
                    Apat = dto.Apat,
                    Amat = dto.Amat,
                    Email = dto.Email,
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
        #region Edita 
        [HttpPut("editar_aceptacion")]
        public IActionResult EditarAceptacion([FromBody] AceptacionDTO dto)
        {
            Aceptacion Aceptacion = _context.Aceptacion.Find(dto.Id);

            if (Aceptacion == null)
            {
                return BadRequest();
            }
            try
            {
                Aceptacion.Id = dto.Id;
                Aceptacion.NombreC = dto.NombreC;
                Aceptacion.idCandidato = dto.idCandidato;
                Aceptacion.Nombre = dto.Nombre;
                Aceptacion.Apat = dto.Apat;
                Aceptacion.Amat = dto.Amat;
                Aceptacion.Email = dto.Email;
                Aceptacion.Fechadenvio = dto.Fechadenvio;
                Aceptacion.Fechaaceptacion = dto.Fechaaceptacion;

                _context.Aceptacion.Update(Aceptacion);
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
        #region Regresa por Email
        [HttpGet("obtener/{email}")]
        public IActionResult ObtenerPorEmail(string email)
        {
            Aceptacion Aceptacionresult = _context.Aceptacion.FirstOrDefault(a => a.Email == email);

            if (Aceptacionresult == null)
            {
                return NotFound("No se encontró la Aceptacion");
            }

            try
            {
                return Ok(new { Aceptacion = Aceptacionresult });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = "Error al procesar la solicitud", error = ex.Message });
            }
        }
        #endregion

    }
}
   
