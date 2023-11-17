using conocelos_v3.Data;
using conocelos_v3.DTOS;
using conocelos_v3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;

namespace conocelos_v3.Controllers
{
        [ApiController]
        [Route("api/consentimiento/")]
        public class ConsentimientoController : Controller
        {
            #region Contexto y datos de la BD
            private readonly ConocelosV2Context _context;
            public ConsentimientoController(ConocelosV2Context context)
            {
                _context = context;
            }
            #endregion

            #region Funcion para obtener 
            [HttpGet("obtener_consentimiento")]
            public IActionResult ObtenerTodosConsentimiento()
            {
                List<ConsentimientoDTO> consentimientoFullResult = new List<ConsentimientoDTO>();
                try
                {
                consentimientoFullResult = (from consentimiento in _context.Consentimiento
                                            select new ConsentimientoDTO()
                                         {
                                             Id = consentimiento.Id,
                                             Nombre = consentimiento.Nombre,
                                             Email = consentimiento.Email,
                                             Estado = consentimiento.Estado,
                                             Fechadenvio = consentimiento.Fechadenvio,
                                             Fechaaceptacion = consentimiento.Fechaaceptacion,
                                             Cuerpocorreo = consentimiento.Cuerpocorreo

                                            }).ToList();
                    return Ok(consentimientoFullResult);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { res = ex });
                }
            }
        #endregion
        #region Funcion que agrega 
        [HttpPost("agregar_consentimiento")]
        public IActionResult AgregarConsentimiento([FromBody] ConsentimientoDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            try
            {
                _context.Consentimiento.Add(new Consentimiento()
                {
                    Id = dto.Id,
                    Nombre = dto.Nombre,
                    Email = dto.Email,
                    Estado = dto.Estado,
                    Fechadenvio = dto.Fechadenvio,
                    Fechaaceptacion = dto.Fechaaceptacion,
                    Cuerpocorreo = dto.Cuerpocorreo,

                });
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { res = ex });
            }
        }
        #endregion
        #region Edita los datos 
        [HttpPut("editar_consentimiento")]
        public IActionResult EditarConsentimiento([FromBody] ConsentimientoDTO dto)
        {
            Consentimiento consentimiento = _context.Consentimiento.Find(dto.Id);

            if (consentimiento == null)
            {
                return BadRequest();
            }
            try
            {
                consentimiento.Id = dto.Id;
                consentimiento.Nombre = dto.Nombre;
                consentimiento.Email = dto.Email;
                consentimiento.Estado = dto.Estado;
                consentimiento.Fechadenvio = dto.Fechadenvio;
                consentimiento.Fechaaceptacion = dto.Fechaaceptacion;
                consentimiento.Cuerpocorreo = dto.Cuerpocorreo;

                _context.Consentimiento.Update(consentimiento);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                // return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, reponse = "error" });
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion
        #region Funcion que elimina
        [HttpDelete("eliminar_consentimiento/{id:int}")]
        public IActionResult EliminarConsentimiento(int id)
        {
            Consentimiento consentimientoDelete = _context.Consentimiento.Find(id);

            if (consentimientoDelete == null)
            {
                return BadRequest();
            }
            try
            {
                _context.Consentimiento.Remove(consentimientoDelete);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        #endregion
        [HttpPost("obtenercorreos")]
        public IActionResult ObtenerCorreoPorEmailDestino(string EmailOrigen, int id, List<string> selectedEmails)
        {
            Correo correoResult = _context.Correo.FirstOrDefault(c => c.EmailOrigen == EmailOrigen);

            if (correoResult == null)
            {
                return NotFound("No se encontró ningún correo con el EmailDestino proporcionado.");
            }

            try
            {

                string EmailOrigen1 = correoResult.EmailOrigen;
                string Contraseña = correoResult.Contraseña;
                string NombreUsuario = correoResult.NombreUsuario;
                int Port = correoResult.PuertoOrigen;
                string Host = correoResult.ServidorOrigen;

                string EmailO = EmailOrigen1;
                string pass = Contraseña;
                string Nombreuser = NombreUsuario;
                string protocolo = "http://";
                string dominio = "localhost:4200";
                string ruta = "/panel/Datos/";
                int id2 = id;
                string parametros = $"{id2}";

                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}";
                Consentimiento consentimientoResult = _context.Consentimiento.Find(id);

                Rol consentimientoId = _context.Rols.Find(consentimientoResult.Id);

                if (consentimientoResult == null)
                {
                    return BadRequest();
                }
                


                foreach (string destinatario in selectedEmails)
                {
                    string Mensaje = protocolo + dominio + ruta + id2;

                    Console.WriteLine("URL generada: " + Mensaje);

                    MailMessage oMailMessage = new MailMessage();
                    oMailMessage.From = new MailAddress(EmailOrigen, NombreUsuario);
                    oMailMessage.To.Add(new MailAddress(destinatario));
                    oMailMessage.Subject = "Notificaciones sistema conoceles";
                    oMailMessage.Body = Mensaje;
                    oMailMessage.IsBodyHtml = true;

                    SmtpClient oSmtpClient = new SmtpClient(Host);
                    oSmtpClient.EnableSsl = true;
                    oSmtpClient.UseDefaultCredentials = false;
                    oSmtpClient.Host = Host;
                    oSmtpClient.Port = Port;
                    oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contraseña);

                    oSmtpClient.Send(oMailMessage);
                }

                Console.WriteLine("Los correos se enviaron con éxito");
                return Ok("Received parameters: emailOrigen=" + EmailOrigen + ", selectedEmails=" + string.Join(",", selectedEmails) + ", id=" + id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #region Regresa por ID
        [HttpGet("datos/{id}")]
        public IActionResult Obtenerconsentimiento(int id)
        {
            Consentimiento consentimientoResult = _context.Consentimiento.Find(id);

            Rol consentimientoId = _context.Rols.Find(consentimientoResult.Id);

            if (consentimientoResult == null)
            {
                return BadRequest();
            }
            try
            {
                return Ok(new { consentimiento = consentimientoResult });
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }
        #endregion

    }
}

    

