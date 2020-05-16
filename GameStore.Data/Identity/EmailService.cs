using Microsoft.AspNet.Identity;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GameStore.Data.Identity
{
    public class EmailService : IEmailService
    {
        private readonly string Host = "smtp.gmail.com";
        private readonly int Port = 587;
        private readonly string From = "gamestore3211@gmail.com";
        private readonly string Pass = "13Avtobusus";

        public Task SendAsync(IdentityMessage message)
        {
         

            SmtpClient client = new SmtpClient(Host, Port)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(From, Pass),
                EnableSsl = true
            };

            var mail = new MailMessage(From, message.Destination)
            {
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = true
            };

            return client.SendMailAsync(mail);
        }
    }
}
