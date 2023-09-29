using conocelos_v3.Data;
using conocelos_v3.Models;
using Microsoft.AspNetCore.Mvc;

namespace conocelos_v3.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class RolController : Controller
    {
        #region Contexto y datos de la BD
        private readonly ConocelosV2Context _context;
        public RolController(ConocelosV2Context context)
        {
            _context = context;
        }
        #endregion

        #region Funcion que obtiene los roles
        [HttpGet("obtener_roles")]
        public IActionResult ObtenerRoles()
        {
            List<Rol> rolesFullResult = new List<Rol>();
            try
            {
                rolesFullResult = _context.Rols.ToList();
                return Ok(rolesFullResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region
        [HttpGet("obtener_rol/{id}")]
        public IActionResult ObtenerRol(int id)
        {
            Rol rol = new Rol();
            try
            {
                rol = _context.Rols.Find(id);
                return Ok(rol);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion
    }
}
