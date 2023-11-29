using conocelos_v3.Data;
using conocelos_v3.DTOS;
using conocelos_v3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace conocelos_v3.Controllers
{
    [ApiController]
    [Route("api/candidato/")]
    public class CandidatoController : Controller
    {
        private readonly ConocelosV2Context _context;

        public CandidatoController(ConocelosV2Context context)
        {
            _context = context;
        }

        [HttpGet("obtener_candidatos")]
        public IActionResult ObtenerTodosCandidatos()
        {
            try
            {
                var candidatos = _context.Candidatos.ToList();
                return Ok(candidatos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener candidatos: {ex.Message}");
            }
        }

        [HttpGet("obtener_candidato/{id}")]
        public IActionResult OptenerCandidato(int id)
        {
            Candidato candidatoResult = _context.Candidatos.Find(id);

            if (candidatoResult == null)
            {
                return Ok(new { response = "Usuario no encontrado" });
            }

            try
            {
                return Ok(candidatoResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener el candidato: {ex.Message}");
            }
        }

        [HttpPost("agregar_candidato")]
        public IActionResult AgregarCandidato([FromBody] CandidatoDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Los datos del candidato son nulos");
            }

            try
            {
                var nuevoCandidato = new Candidato
                {
                    Nombre = dto.Nombre,
                    ApellidoPaterno = dto.ApellidoPaterno,
                    ApellidoMaterno = dto.ApellidoMaterno,
                    SobrenombrePropietario = dto.SobrenombrePropietario,
                    NombreSuplente = dto.NombreSuplente,
                    FechaNacimiento = dto.FechaNacimiento,
                    DireccionCasaCampania = dto.DireccionCasaCampania,
                    TelefonoPublico = dto.TelefonoPublico,
                    Email = dto.Email,
                    PaginaWeb = dto.PaginaWeb,
                    Facebook = dto.Facebook,
                    Twitter = dto.Twitter,
                    Instagram = dto.Instagram,
                    Tiktok = dto.Tiktok,
                    Foto = dto.Foto,
                    Estatus = dto.Estatus,
                    CargoId = dto.CargoId,
                    GeneroId = dto.GeneroId,
                    EstadoId = dto.EstadoId,
                    DistritoLocalId = dto.DistritoLocalId ?? 0,
                    AyuntamientoId = dto.AyuntamientoId ?? 0,
                    ComunidadId = dto.ComunidadId ?? 0,
                    CandidaturaId = dto.CandidaturaId
                };

                // Agregar el nuevo candidato al contexto y guardar los cambios en la base de datos
                _context.Candidatos.Add(nuevoCandidato);
                _context.SaveChanges();


                return Ok();
            }
            catch (Exception ex)
            {
                var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : "No se encontró una excepción interna";
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al agregar el candidato: {ex.Message}. Detalles adicionales: {innerExceptionMessage}");
            }

        }

        [HttpDelete("eliminar_candidato/{id:int}")]
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

    }
}