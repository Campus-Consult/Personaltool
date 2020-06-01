using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Personaltool.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string sentTo, string subject, string message)
        {
            // Hier den E-Mail-Dienst einfügen, um eine E-Mail-Nachricht zu senden.
            string Domain = Options.AuthMessageSenderOptions_Domain;
            string UserName = Options.AuthMessageSenderOptions_UserName;
            string SentFrom = Options.AuthMessageSenderOptions_SentFrom;
            string Password = Options.AuthMessageSenderOptions_Password;
            string SMTPClient = Options.AuthMessageSenderOptions_SMTPClient;

            // Configure the client:
            SmtpClient client = new SmtpClient(SMTPClient);

            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;

            // Create the credentials:
            System.Net.NetworkCredential credentials = new NetworkCredential(UserName, Password, Domain);
            client.EnableSsl = true;
            client.Credentials = credentials;

            // Create the message:
            var mail = new MailMessage(SentFrom, sentTo)
            {
                Subject = subject,
                Body = message
            };

            return client.SendMailAsync(mail);
        }
    }
}