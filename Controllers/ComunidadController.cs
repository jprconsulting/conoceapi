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
    [Route("api/comunidad/")]
    public class ComunidadController : Controller
    {
        private readonly ConocelosV2Context _context;

        public ComunidadController(ConocelosV2Context context)
        {
            _context = context;
        }

        [HttpGet("obtener_comunidades")]
        public IActionResult ObtenerComunidades()
        {
            List<ComunidadDTO> comunidades = _context.Comunidad
                .Select(c => new ComunidadDTO
                {
                    ComunidadId = c.ComunidadId,
                    NombreComunidad = c.NombreComunidad,
                    Acronimo = c.Acronimo,
                    Estatus = c.Estatus,
                    ExtPet = c.ExtPet,
                    AyuntamientoId = c.AyuntamientoId
                })
                .ToList();

            return Ok(comunidades);
        }

        [HttpPost("agregar_comunidad")]
        public IActionResult AgregarComunidad([FromBody] ComunidadDTO comunidadDTO)
        {
            if (comunidadDTO == null)
            {
                return BadRequest();
            }

            Comunidad comunidad = new Comunidad
            {
                NombreComunidad = comunidadDTO.NombreComunidad,
                Acronimo = comunidadDTO.Acronimo,
                Estatus = comunidadDTO.Estatus,
                ExtPet = comunidadDTO.ExtPet,
                AyuntamientoId = comunidadDTO.AyuntamientoId
            };

            _context.Comunidad.Add(comunidad);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("eliminar_comunidad/{id}")]
        public IActionResult EliminarComunidad(int id)
        {
            Comunidad comunidad = _context.Comunidad.Find(id);

            if (comunidad == null)
            {
                return NotFound();
            }

            _context.Comunidad.Remove(comunidad);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut("editar_comunidad")]
        public IActionResult EditarComunidad([FromBody] ComunidadDTO dto)
        {
            try
            {
                var comunidad = _context.Comunidad.Find(dto.ComunidadId);

                if (comunidad == null)
                {
                    return NotFound();
                }

                comunidad.NombreComunidad = dto.NombreComunidad;
                comunidad.Acronimo = dto.Acronimo;
                comunidad.Estatus = dto.Estatus;
                comunidad.ExtPet = dto.ExtPet;
                comunidad.AyuntamientoId = dto.AyuntamientoId;

                _context.Comunidad.Update(comunidad);
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
