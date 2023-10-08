using conocelos_v3.Data;
using conocelos_v3.DTOS;
using conocelos_v3.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace conocelos_v3.Controllers
{
    [ApiController]
    [Route("api/candidatura/")]
    public class CandidaturaController : Controller
    {
        #region Contexto y datos de la BD
        private readonly ConocelosV2Context _context;
        private readonly Utilis _utilis;

        private readonly IWebHostEnvironment _hostingEnvironment;

        public CandidaturaController(ConocelosV2Context context, Utilis utilis, IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _utilis = utilis;
        }
        #endregion

        #region Funcion para obtener a todas las candidaturas
        [HttpGet("obtener_candidaturas")]
        public IActionResult ObtenerTodasCandidaturas()
        {
            List<CandidaturaDTO> candidaturasFullResult = new List<CandidaturaDTO>();
            try
            {
                candidaturasFullResult = (from candidatura in _context.Candidaturas
                                          //join tipoCandidatura in _context.TipoCandidaturas
                                          //on candidatura.CandidaturaId equals tipoCandidatura.TipoCandidaturaId
                                          select new CandidaturaDTO()
                                          {
                                              CandidaturaId = candidatura.CandidaturaId,
                                              TipoCandidaturaId = candidatura.TipoCandidaturaId,
                                              NombreCandidatura = candidatura.NombreCandidatura,
                                              Logo = candidatura.Logo,
                                              Estatus = candidatura.Estatus == true,
                                              acronimo = candidatura.acronimo
                                          }).ToList();

                return Ok(candidaturasFullResult);
            }
            catch (Exception ex)
            {
                // return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = "error" });
                return BadRequest();
            }
        }
        #endregion

        #region Regresa a la candidatura por su ID
        /// <summary>
        /// Optiene solo un candidato que corresponde a su ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        // [Authorize]
        [HttpGet("obtener_candidatura/{id}")]
        public IActionResult OptenerCandidatura(int id)
        {
            Candidatura candidaturaResult = _context.Candidaturas.Find(id);

            if (candidaturaResult == null)
            {
                return BadRequest();
            }
            try
            {
                TipoCandidatura tipoCandidaturaCandidatura = _context.TipoCandidaturas.Find(candidaturaResult.TipoCandidaturaId);

                candidaturaResult.TipoCandidatura = tipoCandidaturaCandidatura;

                return Ok(candidaturaResult);
            }
            catch (Exception ex)
            {
                // return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = "error" });
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        [HttpPost("agregar_candidatura")]
        public async Task<IActionResult> AgregarCandidatura([FromBody] CandidaturaDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            try
            {
                // Convertir la cadena base64 en bytes

                var ruta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                byte[] bytesImagen = Convert.FromBase64String(dto.Base64Logo);
                var rutaDeGuardado = Path.Combine(_hostingEnvironment.WebRootPath, "imagenes");

                if (!Directory.Exists(rutaDeGuardado))
                {
                    Directory.CreateDirectory(rutaDeGuardado);
                }

                // Genera un nombre de archivo único para evitar conflictos
                var nombreArchivo = Guid.NewGuid().ToString() + ".jpg"; // O utiliza la extensión adecuada
                var rutaCompleta = Path.Combine(rutaDeGuardado, nombreArchivo);

                // Guarda la imagen en el servidor
                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                {
                    stream.Write(bytesImagen, 0, bytesImagen.Length);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { response = ex});
            }
        }

        [HttpPost("subir_formfile")]
        public async Task<IActionResult> SubirImagen([FromForm] CandidaturaDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            try
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + dto.imagen.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.imagen.CopyToAsync(stream);
                }


                _context.Candidaturas.Add(new Candidatura()
                {
                    TipoCandidaturaId = dto.TipoCandidaturaId,
                    NombreCandidatura = dto.NombreCandidatura,
                    Logo = dto.imagen.FileName,
                    Estatus = dto.Estatus
                });


                // Aquí puedes guardar el nombre de la imagen en tu base de datos si es necesario.

                return Ok(new { filePath });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost("subir_base64")]
        public IActionResult SubirTexto([FromBody] Candidatura2DTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            try
            {

                _context.Candidaturas.Add(new Candidatura()
                {
                    TipoCandidaturaId = dto.TipoCandidaturaId,
                    NombreCandidatura = dto.NombreCandidatura,
                    Logo = dto.Logo,
                    Estatus = dto.Estatus,
                    acronimo = dto.acronimo
                });
                _context.SaveChanges(); 

                // Devuelve una respuesta de éxito.
                return Ok(new { message = "Datos de texto guardados correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


    }
}
