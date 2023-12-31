﻿using conocelos_v3.Data;
using conocelos_v3.DTOs;
using conocelos_v3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace conocelos_v3.Controllers
{
    [ApiController]
    [Route("api/distritolocal/")]
    public class DistritoLocalController : Controller
    {
        private readonly ConocelosV2Context _context;

        public DistritoLocalController(ConocelosV2Context context)
        {
            _context = context;
        }

        [HttpGet("obtener_distritos")]
        public IActionResult ObtenerDistritos()
        {
            List<DistritoLocal> distritosLocales = _context.DistritoLocal.ToList();
            List<DistritoLocalDTO> distritosLocalesDTO = new List<DistritoLocalDTO>();

            foreach (var distritoLocal in distritosLocales)
            {
                distritosLocalesDTO.Add(new DistritoLocalDTO
                {
                    DistritoLocalId = distritoLocal.DistritoLocalId,
                    NombreDistritoLocal = distritoLocal.NombreDistritoLocal,
                    Acronimo = distritoLocal.Acronimo,
                    Estatus = distritoLocal.Estatus,
                    ExtPet = distritoLocal.ExtPet != null ? distritoLocal.ExtPet : string.Empty, 
                    EstadoId = distritoLocal.EstadoId
                });
            }

            return Ok(distritosLocalesDTO);
        }

        [HttpPost("agregar_distrito")]
        public IActionResult AgregarDistritoLocal([FromBody] DistritoLocal distritoLocal)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                distritoLocal.DistritoLocalId = 0;

                _context.DistritoLocal.Add(distritoLocal);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpDelete("eliminar_distrito/{id}")]
        public IActionResult EliminarDistritoLocal(int id)
        {
            try
            {
                var distritoLocal = _context.DistritoLocal.Find(id);

                if (distritoLocal == null)
                {
                    return NotFound();
                }

                // Verificar dependencias antes de eliminar
                bool tieneAyuntamientos = _context.Ayuntamiento.Any(a => a.DistritoLocalId == id);

                if (tieneAyuntamientos)
                {
                    return BadRequest("No se puede eliminar debido a dependencias");
                }

                _context.DistritoLocal.Remove(distritoLocal);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPut("editar_distrito")]
        public IActionResult EditarDistritoLocal([FromBody] DistritoLocalDTO dto)
        {
            try
            {
                var distritoLocal = _context.DistritoLocal.Find(dto.DistritoLocalId);

                if (distritoLocal == null)
                {
                    return NotFound();
                }

                distritoLocal.NombreDistritoLocal = dto.NombreDistritoLocal;
                distritoLocal.Acronimo = dto.Acronimo;
                distritoLocal.Estatus = dto.Estatus;
                distritoLocal.ExtPet = dto.ExtPet;
                distritoLocal.EstadoId = dto.EstadoId;

                _context.DistritoLocal.Update(distritoLocal);
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
