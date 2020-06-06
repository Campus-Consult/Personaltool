using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Personaltool.Services
{
    /// <summary>
    /// EmailSender, service for sending emails via SMTP
    /// </summary>
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; } // set only via Secret Manager

        public Task SendEmailAsync(string sentTo, string subject, string message)
        {
            // Load service parameters
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