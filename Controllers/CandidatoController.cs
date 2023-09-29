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

        #region Funcion para obtener a todos los candidatos
        [HttpGet("obtener_candidatos")]
        public IActionResult ObtenerTodosCandidatos()
        {
            try
            {
                //List<Candidato> candidatoFullResult = new List<Candidato>();
                //candidatoFullResult = _context.Candidatos.ToList();

                //for (int i = 0; i < candidatoFullResult.LongCount(); i++)
                //{
                //    Candidatura candidaturaCandidato = _context.Candidaturas.Find(candidatoFullResult[i].CandidaturaId);
                //    TipoCandidatura tipoCandidaturaCandidato = _context.TipoCandidaturas.Find(candidaturaCandidato.TipoCandidaturaId);
                //    candidaturaCandidato.TipoCandidatura = tipoCandidaturaCandidato;

                //    Cargo cargoCandidato = _context.Cargos.Find(candidatoFullResult[i].CargoId);

                //    Estado estadoCandidato = _context.Estados.Find(candidatoFullResult[i].EstadoId);

                //    Genero generoCandidato = _context.Generos.Find(candidatoFullResult[i].GeneroId);

                //    candidatoFullResult[i].Candidatura = candidaturaCandidato;
                //    candidatoFullResult[i].Cargo = cargoCandidato;
                //    candidatoFullResult[i].Estado = estadoCandidato;
                //    candidatoFullResult[i].Genero = generoCandidato;
                //}


                //var numeroa = 1;
                //var numerob = 0;

                //var division = numeroa / numerob;

                //List<CandidatoDTO> candidatosFullResult = new List<CandidatoDTO>();

                //candidatosFullResult = (from candidato in _context.Candidatos
                //                        join cargo in _context.Cargos
                //                        on candidato.CargoId equals cargo.CargoId
                //                        join estado in _context.Estados
                //                        on candidato.EstadoId equals estado.EstadoId
                //                        join genero in _context.Generos
                //                        on candidato.GeneroId equals genero.GeneroId
                //                        join candidatura in _context.Candidaturas
                //                        on candidato.CandidaturaId equals candidatura.CandidaturaId

                //                        select new CandidatoDTO()
                //                        {
                //                            CandidatoId = candidato.CandidatoId,
                //                            NombrePropietario = candidato.NombrePropietario,
                //                            SobrenombrePropietario = candidato.SobrenombrePropietario,
                //                            NombreSuplente = candidato.NombreSuplente,
                //                            FechaNacimiento = candidato.FechaNacimiento,
                //                            DireccionCasaCampania = candidato.DireccionCasaCampania,
                //                            TelefonoPublico = candidato.TelefonoPublico,
                //                            Email = candidato.Email,
                //                            PaginaWeb = candidato.PaginaWeb,
                //                            Facebook = candidato.Facebook,
                //                            Twitter = candidato.Twitter,
                //                            Instagram = candidato.Instagram,
                //                            Tiktok = candidato.Tiktok,
                //                            Foto = candidato.Foto,
                //                            Estatus = candidato.Estatus == true,
                //                            // Salto de las relaciones
                //                            Cargo = cargo.NombreCargo,
                //                            Estado = estado.NombreEstado,
                //                            Genero = genero.NombreGenero,
                //                            Candidatura = candidatura.NombreCandidatura
                //                        }).ToList();

                //return Ok(candidatosFullResult);

                var query = from candidato in _context.Candidatos
                            join cargo in _context.Cargos on candidato.CargoId equals cargo.CargoId
                            join estado in _context.Estados on candidato.EstadoId equals estado.EstadoId
                            join genero in _context.Generos on candidato.GeneroId equals genero.GeneroId
                            join candidatura in _context.Candidaturas on candidato.CandidaturaId equals candidatura.CandidaturaId
                            select new
                            {
                                Candidato = candidato,
                                Cargo = cargo,
                                Estado = estado,
                                Genero = genero,
                                Candidatura = candidatura
                            };
                return Ok(query);
            }
            catch (Exception ex)
            {
                return Ok(new { response = ex });
            }                              
        }
        #endregion

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
