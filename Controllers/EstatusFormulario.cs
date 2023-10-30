using conocelos_v3.Data;
using conocelos_v3.DTOS;
using conocelos_v3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace conocelos_v3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstatusFormulario : Controller
    {
        private readonly ConocelosV2Context _context;
        public EstatusFormulario(ConocelosV2Context context)
        {
            _context = context;
        }

        [HttpPost("cambiar_estatus")]
        public ActionResult CambiarEstatus(int formularioUsuarioId)
        {
            var formularioUsuario = _context.GoogleFormUsuarios.Find(formularioUsuarioId);

            if (formularioUsuario != null)
            {
                formularioUsuario.Estatus = true;

                _context.SaveChanges();

                return Ok(); 
            }

            return NotFound(); 
        }

    }
}
