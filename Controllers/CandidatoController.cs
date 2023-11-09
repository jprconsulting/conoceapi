using conocelos_v3.Data;
using conocelos_v3.DTOS;
using conocelos_v3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace conocelos_v3.Controllers
{
    [ApiController]
    [Route("api/candidato/")]
    public class CandidatoController : Controller
    {
        #region Contexto y datos de la BD
        private readonly ConocelosV2Context _context;
        public CandidatoController(ConocelosV2Context context)
        {
            _context = context;
        }
        #endregion

        [HttpGet("obtener_candidatos")]
        public IActionResult ObtenerTodosCandidatos()
        {
            try
            {
                var candidatos = _context.Candidatos.Select(c => new Candidato
                {
                    CandidatoId = c.CandidatoId,
                    Nombre = c.Nombre,
                    ApellidoPaterno = c.ApellidoPaterno,
                    ApellidoMaterno = c.ApellidoMaterno,
                    SobrenombrePropietario = c.SobrenombrePropietario,
                    NombreSuplente = c.NombreSuplente,
                    FechaNacimiento = c.FechaNacimiento,
                    DireccionCasaCampania = c.DireccionCasaCampania,
                    TelefonoPublico = c.TelefonoPublico,
                    Email = c.Email,
                    PaginaWeb = c.PaginaWeb,
                    Facebook = c.Facebook,
                    Twitter = c.Twitter,
                    Instagram = c.Instagram,
                    Tiktok = c.Tiktok,
                    Foto = c.Foto,
                    Estatus = c.Estatus,
                    CargoId = c.CargoId,
                    EstadoId = c.EstadoId,
                    GeneroId = c.GeneroId,
                    CandidaturaId = c.CandidaturaId
                }).ToList();

                return Ok(candidatos);
            }
            catch (Exception ex)
            {
                return Ok(new { response = ex });
            }
        }


        #region Regresa al candidato por su ID
        /// <summary>
        /// Optiene solo un candidato que corresponde a su ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        // [Authorize]
        [HttpGet("obtener_candidato/{id}")]
        public IActionResult OptenerCandidato(int id)
        {
            Candidato candidatoResult = _context.Candidatos.Find(id);

            if (candidatoResult == null)
            {
                return Ok(new {response = "Usuario no encontrado"});
            }

            try
            {
                //Candidatura candidaturaCandidato = _context.Candidaturas.Find(candidatoResult.CandidaturaId);
                //TipoCandidatura tipoCandidaturaCandidato = _context.TipoCandidaturas.Find(candidaturaCandidato.TipoCandidaturaId);
                //candidaturaCandidato.TipoCandidatura = tipoCandidaturaCandidato;

                //Cargo cargoCandidato = _context.Cargos.Find(candidatoResult.CargoId);

                //Estado estadoCandidato = _context.Estados.Find(candidatoResult.EstadoId);

                //Genero generoCandidato = _context.Generos.Find(candidatoResult.GeneroId);

                //candidatoResult.Candidatura = candidaturaCandidato;
                //candidatoResult.Cargo = cargoCandidato;
                //candidatoResult.Estado = estadoCandidato;
                //candidatoResult.Genero = generoCandidato;


                return Ok(candidatoResult);
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { response = ex });
            }
        }
        #endregion

        //#region Funcion que agrega un candidato
        /// <summary>
        /// La funcion agrega a un nuevo candidato 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        // POST api/productos
        // [Authorize]
        //[HttpPost("agregar_candidato")]
        //public IActionResult AgregarCandidato([FromBody] CandidatoDTO dto)
        //{
        //    if (dto == null)
        //    {
        //        return BadRequest();
        //    }
        //    try
        //    {
        //        _context.Candidatos.Add(new Candidato()
        //        {
        //            NombrePropietario = dto.NombrePropietario,
        //            SobrenombrePropietario = dto.SobrenombrePropietario,
        //            NombreSuplente = dto.NombreSuplente,
        //            FechaNacimiento = dto.FechaNacimiento,
        //            DireccionCasaCampania = dto.DireccionCasaCampania,
        //            TelefonoPublico = dto.TelefonoPublico,
        //            Email = dto.Email,
        //            PaginaWeb = dto.PaginaWeb,
        //            Facebook = dto.Facebook,
        //            Twitter = dto.Twitter,
        //            Instagram = dto.Instagram,
        //            Tiktok = dto.Tiktok,
        //            // Foto = dto.Foto,
        //            Estatus = dto.Estatus,
        //            Cargo = dto.CargoId,
        //            Estado = dto.EstadoId,
        //            Genero = dto.GeneroId,
        //            Candidatura = dto.CandidaturaId

        //        });
        //        _context.SaveChanges();
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex);
        //        // return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}
        //#endregion

        //#region Edita los datos de un candidato
        /// <summary>
        /// Edita los datos de un candidato recibiendo como parametro un objeto de tipo candidato
        /// </summary>
        /// <param name="candidatoEdit"></param>
        /// <returns></returns>
        /// 
        // [Authorize]
        //[HttpPut("editar_candidato")]
        //public IActionResult EditarCandidato([FromBody] CandidatoDTO candidatoEdit)
        //{
        //    Candidato candidato = _context.Candidatos.Find(candidatoEdit.CandidatoId);

        //    if (candidato == null)
        //    {
        //        return BadRequest();
        //    }
        //    try
        //    {
        //        candidato.NombrePropietario = candidatoEdit.NombrePropietario;
        //        candidato.SobrenombrePropietario = candidatoEdit.SobrenombrePropietario ;
                
        //        candidato.NombreSuplente  = candidatoEdit.NombreSuplente ;
        //        candidato.FechaNacimiento  = candidatoEdit.FechaNacimiento;
        //        candidato.DireccionCasaCampania  = candidatoEdit.DireccionCasaCampania ;
        //        candidato.TelefonoPublico  = candidatoEdit.TelefonoPublico  ;

        //        candidato.Email = candidatoEdit.Email;
        //        candidato.PaginaWeb = candidatoEdit.PaginaWeb;
        //        candidato.Facebook = candidatoEdit.Facebook;
        //        candidato.Twitter = candidatoEdit.Twitter;
                
        //        candidato.Instagram = candidatoEdit.Instagram;
        //        candidato.Tiktok = candidatoEdit.Tiktok;
        //        candidato.Foto = candidatoEdit.Foto;
        //        candidato.Estatus = candidatoEdit.Estatus ? true : false;

        //        candidato.CargoId = candidatoEdit.CargoId;
        //        candidato.EstadoId = candidatoEdit.EstadoId;
        //        candidato.GeneroId = candidatoEdit.GeneroId;
        //        candidato.CandidaturaId = candidatoEdit.CandidaturaId;

        //        _context.Candidatos.Update(candidato);
        //        _context.SaveChanges();

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(ex);
        //        // return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, reponse = "error" });
        //        // return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}
        //#endregion

        #region Funcion que elimina a un candidato
        /// <summary>
        /// Elimina a un candidato recibiniendo como parametro su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        // [Authorize]
        [HttpDelete("eliminar_candidato")]
        public IActionResult EliminarCandidato(int id)
        {
            Candidato candidatoDelete = _context.Candidatos.Find(id);

            if (candidatoDelete == null)
            {
                return BadRequest();
            }
            try
            {
                _context.Candidatos.Remove(candidatoDelete);
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
