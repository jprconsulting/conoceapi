using conocelos_v3.Data;
using conocelos_v3.Models;
using conocelos_v3.Services;
using conocelos_v3.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace conocelos_v3.Controllers
{
    [ApiController]
    [Route("api/tabla_formulario/")]
    public class TablaFormularioController : Controller
    {
        #region El contexto de las bases de datos : Interface de conexion
        private readonly ConocelosV2Context _context;
        private readonly IConvertJson _convertJson;

        public TablaFormularioController(ConocelosV2Context context, IConvertJson convertJsonStringJson)
        {
            _context = context;
            _convertJson = convertJsonStringJson;
        }
        #endregion

        #region Funcion para obtener a todos los candidatos
        [HttpGet("obtener_formularios")]
        public IActionResult ObtenerTodosFormularios()
        {
            List<TablaFormulario> formulariosFullResult = new List<TablaFormulario>();
            try
            {
                formulariosFullResult = _context.TablaFormularios.ToList();

                return Ok(formulariosFullResult);
            }
            catch (Exception ex)
            {
                // return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = "error" });
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region Regresa al formulario por su ID
        /// <summary>
        /// Optiene solo un formulario que corresponde a su ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        // [Authorize]
        [HttpGet("obtener_formulario/{id}")]
        public IActionResult OptenerFormulario(int id)
        {
            TablaFormulario formularioResult = _context.TablaFormularios.Find(id);

            if (formularioResult == null)
            {
                return BadRequest();
            }
            try
            {
                return Ok(formularioResult);
            }
            catch (Exception ex)
            {
                // return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = "error" });
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region Funcion que agrega un formulario
        /// <summary>
        /// La funcion agrega a un nuevo candidato 
        /// </summary>
        /// <param name="candidateAdd"></param>
        /// <returns></returns>
        // POST api/productos
        // [Authorize]
        [HttpPost("agregar_formulario")]
        public IActionResult AgregarFormulario([FromForm] TablaFormularioFront formularioNewFront)
        {
            try
            {
                string RutaFile = formularioNewFront.ArchvioJson.FileName;

                // Obtener el metodo para deserializar el archivo
                var document = _convertJson.ConvertirDataJson(RutaFile);

                TablaFormulario formCompleteNewRegister = new TablaFormulario()
                {
                    FormName = formularioNewFront.FormNameFront,
                    GoogleFormId = formularioNewFront.GoogleFormIdFront,
                    GoogleEditFormId = formularioNewFront.GoogleEditFormIdFront,
                    SpreadsheetId = formularioNewFront.SpreadsheetIdFront,
                    SheetName = formularioNewFront.SheetNameFront,
                    ProjectId = formularioNewFront.ProjectIdFront,
                    // Inicia la parte del Back
                    Type = document.Result.type,
                    ProjectId1 = document.Result.project_id,
                    PrivateKeyId = document.Result.private_key_id,
                    // 3
                    PrivateKey = document.Result.private_key,
                    ClientEmail = document.Result.client_email,
                    ClientId = document.Result.client_id,
                    // 6
                    AuthUri = document.Result.auth_uri,
                    TokenUri = document.Result.token_uri,
                    AuthProviderX509CertUrl = document.Result.auth_provider_x509_cert_url,
                    // 9
                    ClientX509CertUrl = document.Result.client_x509_cert_url,
                    UniverseDomain = document.Result.universe_domain
                    // 11
                };


                if (formularioNewFront == null)
                {
                    return BadRequest();
                }

                _context.TablaFormularios.Add(formCompleteNewRegister);
                _context.SaveChanges();

                return Ok("Fomulario Agregado !");
            }
            catch (Exception ex)
            {
                // return StatusCode(StatusCodes.Status500InternalServerError);
                return Ok(ex);
            }
        }
        #endregion

        #region Funcion que elimina a un formulario
        /// <summary>
        /// Elimina a un candidato recibiniendo como parametro su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        // [Authorize]
        [HttpDelete("eliminar_formulario")]
        public IActionResult EliminarCandidato(int id)
        {
            TablaFormulario formularioDelete = _context.TablaFormularios.Find(id);

            if (formularioDelete == null)
            {
                return BadRequest();
            }
            try
            {
                _context.TablaFormularios.Remove(formularioDelete);
                _context.SaveChanges();

                return Ok("Formulario Eliminado ! ");
            }
            catch (Exception ex)
            {
                // return StatusCode(StatusCodes.Status500InternalServerError);
                return Ok(ex);
            }
        }
        #endregion

    }
}
