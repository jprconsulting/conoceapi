using conocelos_v3.Data;
using conocelos_v3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace conocelos_v3.Controllers
{
    [ApiController]
    [Route("api/cargo/")]
    public class CargoController : Controller
    {
        #region Contexto y datos de la BD
        private readonly ConocelosV2Context _context;
        public CargoController(ConocelosV2Context context)
        {
            _context = context;
        }
        #endregion

        #region Funcion para obtener a todos los cargos
        [HttpGet("obtener_cargos")]
        public IActionResult ObtenerTodosCandidatos()
        {
            List<Cargo> cargosFullResult = new List<Cargo>();
            try
            {
                cargosFullResult = _context.Cargos.ToList();
                return Ok(cargosFullResult);
            }
            catch (Exception ex)
            {
                // return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = "error" });
                return StatusCode(StatusCodes.Status500InternalServerError, new { response = ex});
            }
        }
        #endregion

        #region Regresa al cargo por su ID
        /// <summary>
        /// Optiene solo un candidato que corresponde a su ID 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        // [Authorize]
        [HttpGet("obtener_cargo")]
        public IActionResult GetCandidateId(int id)
        {
            Cargo cargoResult = _context.Cargos.Find(id);

            if (cargoResult == null)
            {
                return BadRequest();
            }
            try
            {
                return Ok(cargoResult);
            }
            catch (Exception ex)
            {
                // return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = "error" });
                return StatusCode(StatusCodes.Status500InternalServerError, new { response = ex });
            }
        }
        #endregion

        #region Funcion que agrega un cargo
        /// <summary>
        /// La funcion agrega a un nuevo candidato 
        /// </summary>
        /// <param name="cargoNew"></param>
        /// <returns></returns>
        // POST api/productos
        // [Authorize]
        [HttpPost("agregar_cargo")]
        public IActionResult AppendCandidate([FromBody] Cargo cargoNew)
        {
            try
            {
                _context.Cargos.Add(cargoNew);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { response = ex });
            }
        }
        #endregion

        #region Edita los datos de un cargo
        /// <summary>
        /// Edita los datos de un candidato recibiendo como parametro un objeto de tipo candidato
        /// </summary>
        /// <param name="cargoEdit"></param>
        /// <returns></returns>
        /// 
        // [Authorize]
        [HttpPut("editar_cargo")]
        public IActionResult EditCandidate([FromBody] Cargo cargoEdit)
        {
            Cargo cargo = _context.Cargos.Find(cargoEdit.CargoId);

            if (cargo == null)
            {
                return BadRequest();
            }
            try
            {
                cargo.NombreCargo = cargoEdit.NombreCargo is null ? cargo.NombreCargo : cargo.NombreCargo;

                _context.Cargos.Update(cargo);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                // return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, reponse = "error" });
                return StatusCode(StatusCodes.Status500InternalServerError, new { response = ex });
            }
        }
        #endregion

        #region Funcion que elimina a un cargo
        /// <summary>
        /// Elimina a un candidato recibiniendo como parametro su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        // [Authorize]
        [HttpDelete("eliminar_cargo")]
        public IActionResult DeleteCandidate(int id)
        {
            Cargo cargoDelete = _context.Cargos.Find(id);

            if (cargoDelete == null)
            {
                return BadRequest();
            }
            try
            {
                _context.Cargos.Remove(cargoDelete);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        #endregion
    }
}
