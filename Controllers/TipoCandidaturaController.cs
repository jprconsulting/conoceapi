using conocelos_v3.Data;
using conocelos_v3.Models;
using Microsoft.AspNetCore.Mvc;

namespace conocelos_v3.Controllers
{
    [ApiController]
    [Route("api/tipo_candidatura")]
    public class TipoCandidaturaController : Controller
    {

        #region Contexto y datos de la BD
        private readonly ConocelosV2Context _context;
        public TipoCandidaturaController(ConocelosV2Context context)
        {
            _context = context;
        }
        #endregion


        #region Funcion que obtiene los tipos de candidaturas
        [HttpGet("obtener_tipos_candidaturas")]
        public IActionResult ObtenerTiposCandidaturas()
        {
            List<TipoCandidatura> tiposCandidaturasFullResult = new List<TipoCandidatura>();
            try
            {
                tiposCandidaturasFullResult = _context.TipoCandidaturas.ToList();
                return Ok(tiposCandidaturasFullResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region
        [HttpGet("obtener_tipo_candidatura/{id}")]
        public IActionResult ObtenerTipoCandidatura(int id)
        {
            TipoCandidatura tipoCandidatura = new TipoCandidatura();
            try
            {
                tipoCandidatura = _context.TipoCandidaturas.Find(id);
                return Ok(tipoCandidatura);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion
    }
}
