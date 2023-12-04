using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using conocelos_v3.DTOS;
using conocelos_v3.Models;
using conocelos_v3.Data;

namespace conocelos_v3.Controllers
{
    [ApiController]
    [Route("api/candidato")]
    public class CandidatoController : ControllerBase
    {
        private readonly ConocelosV2Context _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CandidatoController(ConocelosV2Context context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> AgregarCandidato([FromForm] Candidato candidato)
        {
            if (candidato == null)
            {
                return BadRequest("Los datos del candidato son nulos");
            }

            try
            {
                string rutaImagenes = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                if (!Directory.Exists(rutaImagenes))
                {
                    Directory.CreateDirectory(rutaImagenes);
                }

                if (Request.Form.Files.Count > 0) // Verificar si hay archivos adjuntos
                {
                    var fotoArchivo = Request.Form.Files[0]; // Obtener la imagen adjunta

                    if (fotoArchivo.Length > 0)
                    {
                        string nombreArchivo = $"{Guid.NewGuid()}.jpg";
                        string rutaCompleta = Path.Combine(rutaImagenes, nombreArchivo);

                        using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                        {
                            await fotoArchivo.CopyToAsync(stream); // Guardar la imagen en el servidor
                        }

                        candidato.Foto = nombreArchivo;
                    }
                }

                _context.Candidatos.Add(candidato);
                await _context.SaveChangesAsync();

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