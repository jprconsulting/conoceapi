
using System;
using System.Net.Mail;

namespace conocelos_v3.Email
{
    public class Email
    {
        static void Main(string[] args)
        {
            string EmailOrigen = "fjfmjr1@gmail.com";
            string EmailDestino = "moraazul12501@gmail.com";
            string Contraseña = "hvfl mibv jgfl eozk";
            string Nombreuser = "Fabian";
            // emailorigen, EmailDestino, asunto, mensaje
            MailMessage oMailMessage = new MailMessage();
            oMailMessage.From = new MailAddress(EmailOrigen, Nombreuser);
            oMailMessage.To.Add(new MailAddress(EmailDestino));
            oMailMessage.Subject = "MensajeImportante";
            oMailMessage.Body = "<p>Mensaje 1</p><p>Mensaje2</p>";

            oMailMessage.IsBodyHtml = true;
            //inicializamos el contructor
            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            oSmtpClient.EnableSsl = true;
            //Credenciales
            oSmtpClient.UseDefaultCredentials = false;
            //Host
            oSmtpClient.Host = "smtp.gmail.com";
            //Puerto
            oSmtpClient.Port = 587;
            //Credenciales
            oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contraseña);
            //Enviamos objeto mensaje
            oSmtpClient.Send(oMailMessage);
            //Eliminar
            oSmtpClient.Dispose();
            Console.WriteLine("Ya se envio");
        }
    }
}
