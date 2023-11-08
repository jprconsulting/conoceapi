using conocelos_v3.Data;
using conocelos_v3.DTOS;
using conocelos_v3.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace conocelos_v3.Controllers
{
    [Route("api/respuestas-google-formulario")]
    [ApiController]
    public class RespuestasGoogleFormularioController : Controller
    {
        private readonly ConocelosV2Context _context;
        public RespuestasGoogleFormularioController(ConocelosV2Context context)
        {
            _context = context;
        }

        [HttpGet("respuestas-formulario/{googleFormId}")]
        public async Task<IActionResult> RespuestasFormulario(string googleFormId)
        {
            var formulario = await _context.GoogleForms.FirstOrDefaultAsync(g => g.GoogleFormId == googleFormId);

            if (formulario != null)
            {
                var credentials = new CredentialsJSON
                {
                    type = formulario.Type,
                    project_id = formulario.ProjectId,
                    private_key_id = formulario.PrivateKeyId,
                    private_key = formulario.PrivateKey,
                    client_email = formulario.ClientEmail,
                    client_id = formulario.ClientId,
                    auth_uri = formulario.AuthUri,
                    token_uri = formulario.TokenUri,
                    auth_provider_x509_cert_url = formulario.AuthProviderX509CertUrl,
                    client_x509_cert_url = formulario.ClientX509CertUrl,
                    universe_domain = formulario.UniverseDomain
                };

                string json = JsonConvert.SerializeObject(credentials, Formatting.Indented);

                // Configura las credenciales de autenticación (OAuth 2.0 o API key)
                GoogleCredential credential = GoogleCredential.FromJson(json);

                // Crea el servicio de Google Sheets
                SheetsService sheetsService = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Test-conoceles"
                });

                // ID de la hoja de cálculo vinculada al formulario de Google
                string spreadsheetId = formulario.SpreadsheetId;

                // Rango de datos que deseas obtener (por ejemplo, "Sheet1!A:F" para todas las columnas de la hoja "Sheet1")
                string range = $"{formulario.SheetName}!A1:F";

                // Realiza la solicitud para obtener los datos
                SpreadsheetsResource.ValuesResource.GetRequest request =
                    sheetsService.Spreadsheets.Values.Get(spreadsheetId, range);

                ValueRange response = request.Execute();

                IList<IList<object>> values = response.Values;
                if (values != null && values.Count > 0)
                {
                    var preguntasIndex = new List<PreguntaIndexDTO>();

                    var nombresPreguntas = values[0];

                    for (int i = 0; i < nombresPreguntas.Count; i++)
                    {
                        string nombrePregunta = nombresPreguntas[i]?.ToString() ?? "";
                        var existePregunta = await _context.PreguntaCuestionarioGoogleForms
                            .FirstOrDefaultAsync(p => p.FormularioId == formulario.FormularioId && p.Pregunta == nombrePregunta);

                        // !string.IsNullOrWhiteSpace(nombrePregunta)
                        if (existePregunta == null)
                        {
                            var preguntaAdd = new PreguntaCuestionarioGoogleForm()
                            {
                                FormularioId = formulario.FormularioId,
                                Pregunta = nombrePregunta
                            };

                            _context.PreguntaCuestionarioGoogleForms.Add(preguntaAdd);
                            await _context.SaveChangesAsync();
                            preguntasIndex.Add(new PreguntaIndexDTO { PreguntaDBId = preguntaAdd.PreguntaCuestionarioId, Index = i });
                        }
                        else
                        {
                            preguntasIndex.Add(new PreguntaIndexDTO { PreguntaDBId = existePregunta.PreguntaCuestionarioId, Index = i });
                        }

                    }

                    // Procesa las filas de respuestas (values[1] en adelante)
                    for (int rowIndex = 1; rowIndex < values.Count; rowIndex++)
                    {
                        var respuestas = values[rowIndex];

                        var email = respuestas[1];

                        var existeCandidato = await _context.Candidatos.FirstOrDefaultAsync(c => string.Equals(c.Email, email));

                        if (existeCandidato != null)
                        {
                            for (int i = 0; i < respuestas.Count; i++)
                            {
                                string respuestaCandidato = respuestas[i]?.ToString() ?? "";
                                int preguntaCuestionarioIdDB = preguntasIndex.FirstOrDefault(p => p.Index == i)?.PreguntaDBId ?? 0;
                                var existeRespuesta = await _context.RespuestaPreguntaCuestionarioGoogleForms
                                    .FirstOrDefaultAsync(r => r.PreguntaCuestionarioId == preguntaCuestionarioIdDB && r.CandidatoId == existeCandidato.CandidatoId);


                                if (existeRespuesta != null)
                                {
                                    existeRespuesta.Respuesta = respuestaCandidato;
                                }
                                else
                                {
                                    _context.RespuestaPreguntaCuestionarioGoogleForms.Add(new RespuestaPreguntaCuestionarioGoogleForm()
                                    {
                                        PreguntaCuestionarioId = preguntaCuestionarioIdDB,
                                        Respuesta = respuestaCandidato,
                                        CandidatoId = existeCandidato.CandidatoId

                                    });
                                }

                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
