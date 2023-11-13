using conocelos_v3.Data;
using conocelos_v3.DTOS;
using conocelos_v3.Models;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;
using System.Globalization;
using System;

namespace conocelos_v3.Controllers
{
    [ApiController]
    [Route("api/correo/")]
    public class CorreoController : Controller
    {
        #region Contexto y datos de la BD
        private readonly ConocelosV2Context _context;
        public CorreoController(ConocelosV2Context context)
        {
            _context = context;
        }
        #endregion

        #region Funcion para obtener 
        [HttpGet("obtener_correo")]
        public IActionResult ObtenerTodosCorreos()
        {
            List<CorreoDTO> correosFullResult = new List<CorreoDTO>();
            try
            {
                correosFullResult = (from correo in _context.Correo
                                     select new CorreoDTO()
                                     {
                                         Id = correo.Id,
                                         EmailOrigen = correo.EmailOrigen,
                                         Contraseña = correo.Contraseña,
                                         Credenciales = correo.Credenciales,
                                         NombreUsuario = correo.NombreUsuario,
                                         ServidorOrigen = correo.ServidorOrigen,
                                         PuertoOrigen = correo.PuertoOrigen,
                                         ConfiarCertificado = correo.ConfiarCertificado,
                                         PerfilCorreo = correo.PerfilCorreo
                                     }).ToList();
                return Ok(correosFullResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { res = ex });
            }
        }
        #endregion
        [HttpPost("obtenercorreos/{id}")]
        public IActionResult ObteneridCorreo(int id, string EmailDestino, string Mensaje)
        {
            Correo correoResult = _context.Correo.Find(id);

            if (correoResult == null)
            {
                return BadRequest();
            }
            try
            {

                string EmailOrigen = correoResult.EmailOrigen;
                string Contraseña = correoResult.Contraseña;
                string NombreUsuario = correoResult.NombreUsuario;
                int Port = correoResult.PuertoOrigen;
                string Host = correoResult.ServidorOrigen;

                string EmailO = EmailOrigen;
                string pass = Contraseña;
                string Nombreuser = NombreUsuario;
                try
                {
                    Ping ping = new Ping();
                    PingReply reply = ping.Send(Host);

                    if (reply.Status != IPStatus.Success)
                    {
                        // SMTP server is not reachable
                        Console.WriteLine("Puerto invalido");
                        return BadRequest("Puerto invalido");
                    }
                }
                catch (PingException ex)
                {
                    // Handle ping-related exceptions
                    Console.WriteLine("Servidor invalido");
                    return BadRequest("Servidor invalido");
                }

                // Check if the port is valid
                if (Port < 1 || Port > 590)
                {
                    // Handle invalid port
                    Console.WriteLine("Puerto invalido");
                    return BadRequest("Puerto invalido");
                }
                
                MailMessage oMailMessage = new MailMessage();
                oMailMessage.From = new MailAddress(EmailO, Nombreuser);
                oMailMessage.To.Add(new MailAddress(EmailDestino));
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
                oSmtpClient.Dispose();
                Console.WriteLine("El correo se envió con éxito");
                return Ok(new { correo = correoResult });

            }

            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el correo: " + ex.Message);
                string mensajeError = ex.Message;

                if (mensajeError.Contains("The SMTP server requires a secure connection or the client was not authenticated. The server response was: 5.7.0 Authentication Required"))
                {
                    mensajeError = "Se requiere una conexión segura o el cliente no ha sido autenticado(error en email o contraseña).";
                }
                else
                {
                    mensajeError = "Error inesperado: " + mensajeError;
                }
                
                return BadRequest("Email no enviado" + mensajeError);
            }
            







        }

        #region Funcion que agrega
        [HttpPost("agregar_correo")]
        public IActionResult AgregarCorreo([FromBody] CorreoDTO dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }
            try
            {
                _context.Correo.Add(new Correo()
                {
                    Id = dto.Id,
                    EmailOrigen = dto.EmailOrigen,
                    Contraseña = dto.Contraseña,
                    Credenciales = dto.Credenciales,
                    NombreUsuario = dto.NombreUsuario,
                    ServidorOrigen = dto.ServidorOrigen,
                    PuertoOrigen = dto.PuertoOrigen,
                    ConfiarCertificado = dto.ConfiarCertificado,
                    PerfilCorreo = dto.PerfilCorreo

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
        #region Edita los datos de un usuario
        [HttpPut("editarcorreo")]
        public IActionResult EditarCorreo([FromBody] CorreoDTO dto)
        {
            Correo correo = _context.Correo.Find(dto.Id);

            if (correo == null)
            {
                return BadRequest();
            }
            try
            {
                correo.Id = dto.Id;
                correo.EmailOrigen = dto.EmailOrigen;
                correo.Contraseña = dto.Contraseña;
                correo.Credenciales = dto.Credenciales;
                correo.NombreUsuario = dto.NombreUsuario;
                correo.ServidorOrigen = dto.ServidorOrigen;
                correo.PuertoOrigen = dto.PuertoOrigen;
                correo.ConfiarCertificado = dto.ConfiarCertificado;
                correo.PerfilCorreo = dto.PerfilCorreo;
                _context.Correo.Update(correo);
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
        [HttpDelete("eliminar_correo/{id:int}")]
        public IActionResult EliminarCorreo(int id)
        {
            Correo correoDelete = _context.Correo.Find(id);

            if (correoDelete == null)
            {
                return BadRequest();
            }
            try
            {
                _context.Correo.Remove(correoDelete);
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
