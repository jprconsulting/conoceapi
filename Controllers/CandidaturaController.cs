using conocelos_v3.Data;
using conocelos_v3.DTOS;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using conocelos_v3.Models;

namespace conocelos_v3.Controllers
{
    [ApiController]
    [Route("api/candidatura/")]
    public class CandidaturaController : Controller
    {
        private readonly ConocelosV2Context _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CandidaturaController(ConocelosV2Context context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("obtener_candidaturas")]
        public IActionResult ObtenerTodasCandidaturas()
        {
            try
            {
                var candidaturasFullResult = _context.Candidaturas.Select(candidatura => new CandidaturaDTO
                {
                    CandidaturaId = candidatura.CandidaturaId,
                    TipoCandidaturaId = candidatura.TipoCandidaturaId,
                    NombreCandidatura = candidatura.NombreCandidatura,
                    Logo = candidatura.Logo,
                    Estatus = candidatura.Estatus,
                    Acronimo = candidatura.Acronimo,
                    Foto = candidatura.Foto
                }).ToList();

                return Ok(candidaturasFullResult);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("obtener_candidatura/{id}")]
        public IActionResult ObtenerCandidatura(int id)
        {
            try
            {
                var candidatura = _context.Candidaturas.Find(id);

                if (candidatura == null)
                {
                    return BadRequest();
                }

                var tipoCandidatura = _context.TipoCandidaturas.Find(candidatura.TipoCandidaturaId);

                candidatura.TipoCandidatura = tipoCandidatura;

                return Ok(candidatura);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("agregar_candidatura")]
        public IActionResult AgregarCandidatura([FromForm] CandidaturaDTO nuevaCandidatura)
        {
            try
            {
                var candidatura = new Candidatura
                {
                    TipoCandidaturaId = nuevaCandidatura.TipoCandidaturaId,
                    NombreCandidatura = nuevaCandidatura.NombreCandidatura,
                    Logo = nuevaCandidatura.Logo,
                    Estatus = nuevaCandidatura.Estatus,
                    Acronimo = nuevaCandidatura.Acronimo
                };

                if (nuevaCandidatura.FotoArchivo != null && nuevaCandidatura.FotoArchivo.Length > 0)
                {
                    string nombreArchivo = $"{Guid.NewGuid()}.jpg";
                    string rutaCompleta = Path.Combine(_hostingEnvironment.WebRootPath, "images", nombreArchivo);

                    using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                    {
                        nuevaCandidatura.FotoArchivo.CopyTo(stream);
                    }

                    candidatura.Foto = nombreArchivo;
                }

                _context.Candidaturas.Add(candidatura);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al agregar la candidatura: {ex.Message}");
            }
        }
    }
}
