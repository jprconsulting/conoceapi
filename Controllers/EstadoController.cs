using conocelos_v3.Data;
using conocelos_v3.Models;
using Microsoft.AspNetCore.Mvc;

namespace conocelos_v3.Controllers
{
    [ApiController]
    [Route("api/estado/")]
    public class EstadoController : Controller
    {
        #region El contexto de la base de datos 
        private readonly ConocelosV2Context _context;
        public EstadoController(ConocelosV2Context context)
        {
            _context = context;
        }
        #endregion


        #region Funcion que obtiene los estados
        [HttpGet("obtener_estados")]
        public IActionResult ObtenerEstados()
        {
            List<Estado> estadosFullResult = new List<Estado>();
            try
            {
                estadosFullResult = _context.Estados.ToList();
                return Ok(estadosFullResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region
        [HttpGet("obtener_estado/{id}")]
        public IActionResult ObtenerEstado(int id)
        {
            Estado estado = new Estado();
            try
            {
                estado = _context.Estados.Find(id);
                return Ok(estado);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion
    }
}
