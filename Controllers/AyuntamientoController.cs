using conocelos_v3.Data;
using conocelos_v3.DTOS;
using conocelos_v3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace conocelos_v3.Controllers
{
    [ApiController]
    [Route("api/ayuntamiento/")]
    public class AyuntamientoController : Controller
    {
        private readonly ConocelosV2Context _context;

        public AyuntamientoController(ConocelosV2Context context)
        {
            _context = context;
        }

        [HttpGet("obtener_ayuntamientos")]
        public IActionResult ObtenerAyuntamientos()
        {
            List<AyuntamientoDTO> ayuntamientos = _context.Ayuntamiento
                .Select(a => new AyuntamientoDTO
                {
                    AyuntamientoId = a.AyuntamientoId,
                    NombreAyuntamiento = a.NombreAyuntamiento,
                    Acronimo = a.Acronimo,
                    Estatus = a.Estatus,
                    ExtPet = a.ExtPet,
                    DistritoLocalId = a.DistritoLocalId
                })
                .ToList();

            return Ok(ayuntamientos);
        }

        [HttpPost("agregar_ayuntamiento")]
        public IActionResult AgregarAyuntamiento([FromBody] AyuntamientoDTO ayuntamientoDTO)
        {
            if (ayuntamientoDTO == null)
            {
                return BadRequest();
            }

            Ayuntamiento ayuntamiento = new Ayuntamiento
            {
                NombreAyuntamiento = ayuntamientoDTO.NombreAyuntamiento,
                Acronimo = ayuntamientoDTO.Acronimo,
                Estatus = ayuntamientoDTO.Estatus,
                ExtPet = ayuntamientoDTO.ExtPet,
                DistritoLocalId = ayuntamientoDTO.DistritoLocalId
            };

            _context.Ayuntamiento.Add(ayuntamiento);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("eliminar_ayuntamiento/{id}")]
        public IActionResult EliminarAyuntamiento(int id)
        {
            Ayuntamiento ayuntamiento = _context.Ayuntamiento.Find(id);

            if (ayuntamiento == null)
            {
                return NotFound();
            }

            _context.Ayuntamiento.Remove(ayuntamiento);
            _context.SaveChanges();

            return Ok();
        }
    }
}
