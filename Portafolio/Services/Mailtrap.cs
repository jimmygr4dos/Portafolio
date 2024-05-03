using Portafolio.Models;
using System.Net.Mail;
using System.Net;

namespace Portafolio.Services
{
    public interface IMailTrap
    {
        Task Send(ContactoViewModel contact);
    }
    public class Mailtrap: IMailTrap
    {
        public IConfiguration _configuration { get; }
        public Mailtrap(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Send(ContactoViewModel contact)
        {
            var username = _configuration.GetValue<string>("MAILTRAP_USERNAME");
            var password = _configuration.GetValue<string>("MAILTRAP_PASSWORD");
            var email = _configuration.GetValue<string>("MAILTRAP_FROM");

            var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential($"{username}", $"{password}"),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(email),
                Subject = $"El cliente {contact.Nombre} quiere contactarse contigo",
                IsBodyHtml = true,
                Body = $"<p>{contact.Mensaje}</p>"
            };
            mailMessage.To.Add(contact.Email);

            await client.SendMailAsync(mailMessage);
        }

    }
}
