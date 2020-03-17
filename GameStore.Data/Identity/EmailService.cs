using Microsoft.AspNet.Identity;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GameStore.Data.Identity
{
    public class EmailService : IEmailService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var host = "game.store";
            var port = 25;
            var from = "admin@game.store";
            var pass = "admin123";

            SmtpClient client = new SmtpClient(host, port)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(from, pass),
                EnableSsl = true
            };

            var mail = new MailMessage(from, message.Destination)
            {
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = true
            };

            return client.SendMailAsync(mail);
        }
    }
}
