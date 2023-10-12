using conocelos_v3.Data;
using conocelos_v3.DTOS;
using conocelos_v3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace conocelos_v3.Controllers
{
    [Route("api/config-google-form")]
    [ApiController]
    public class ConfigGoogleFormController : Controller
    {
        private readonly ConocelosV2Context _context;
        public ConfigGoogleFormController(ConocelosV2Context context)
        {
            _context = context;
        }

        [HttpGet("get-config-google-forms")]
        public IActionResult GetConfigGoogleForms()
        {
            try
            {
                var formulariosFullResult = _context.GoogleForms.Select(g => new GoogleFormDTO()
                {
                    FormularioId = g.FormularioId,
                    FormName = g.FormName,
                    GoogleFormId = g.GoogleFormId,
                    SpreadsheetId = g.SpreadsheetId,
                    SheetName = g.SheetName,
                    Type = g.Type,
                    ProjectId = g.ProjectId,
                    PrivateKeyId = g.PrivateKeyId,
                    PrivateKey = g.PrivateKey,
                    ClientEmail = g.ClientEmail,
                    ClientId = g.ClientId,
                    AuthUri = g.AuthUri,
                    TokenUri = g.TokenUri,
                    AuthProviderX509CertUrl = g.AuthProviderX509CertUrl,
                    ClientX509CertUrl = g.ClientX509CertUrl,
                    UniverseDomain = g.UniverseDomain
                }).ToList();

                return Ok(formulariosFullResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("post-config-google-forms")]
        public IActionResult PostConfigGoogleForms([FromBody] GoogleFormDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            try
            {
                _context.GoogleForms.Add(new GoogleForm()
                {
                    FormName = dto.FormName,
                    GoogleFormId = dto.GoogleFormId,
                    SpreadsheetId = dto.SpreadsheetId,
                    SheetName = dto.SheetName,
                    Type = dto.Type,
                    ProjectId = dto.ProjectId,
                    PrivateKeyId = dto.PrivateKeyId,
                    PrivateKey = dto.PrivateKey,
                    ClientEmail = dto.ClientEmail,
                    ClientId = dto.ClientId,
                    AuthUri = dto.AuthUri,
                    TokenUri = dto.TokenUri,
                    AuthProviderX509CertUrl = dto.AuthProviderX509CertUrl,
                    ClientX509CertUrl = dto.ClientX509CertUrl,
                    UniverseDomain = dto.UniverseDomain
                });

                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { res = ex });
            }
        }

        [HttpDelete("eliminar_formulario/{formularioId}")]
        public IActionResult DeleteFormulario(int formularioId)
        {
            try
            {
                var formulario = _context.GoogleForms.FirstOrDefault(g => g.FormularioId == formularioId);

                if (formulario == null)
                {
                    return NotFound();
                }

                _context.GoogleForms.Remove(formulario);
                _context.SaveChanges();

                return NoContent(); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { res = ex });
            }
        }

        [HttpPut("actualizar-formulario")]
        public IActionResult PutConfigGoogleForm([FromBody] GoogleFormDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            try
            {
                var formulario = _context.GoogleForms.Find(dto.FormularioId);

                if (formulario == null)
                {
                    return BadRequest();
                }

                formulario.FormName = dto.FormName;
                formulario.GoogleFormId = dto.GoogleFormId;
                formulario.SpreadsheetId = dto.SpreadsheetId;
                formulario.SheetName = dto.SheetName;
                formulario.Type = dto.Type;
                formulario.ProjectId = dto.ProjectId;
                formulario.PrivateKeyId = dto.PrivateKeyId;
                formulario.PrivateKey = dto.PrivateKey;
                formulario.ClientEmail = dto.ClientEmail;
                formulario.ClientId = dto.ClientId;
                formulario.AuthUri = dto.AuthUri;
                formulario.TokenUri = dto.TokenUri;
                formulario.AuthProviderX509CertUrl = dto.AuthProviderX509CertUrl;
                formulario.ClientX509CertUrl = dto.ClientX509CertUrl;
                formulario.UniverseDomain = dto.UniverseDomain;

                _context.GoogleForms.Update(formulario);
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
