using EmailSend.Model;
using EmailSend.settings;
using Entity.Entity;
using MailKit.Security;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace EmailSend.Service
{
    public class EmailService : IEmailService
    {
        private readonly EntityDbcontext _entityDbcontext;
        
        private readonly EmaillSettings _mailSettings;
        public EmailService(IOptions<EmaillSettings> mailSettings, EntityDbcontext entityDbcontext)
        {
            _mailSettings = mailSettings.Value;
            _entityDbcontext = entityDbcontext;
        }

        public async Task SendEmailAsync()
        {
            var recipients = _entityDbcontext.EmployeeEmail.Select(e => e.Emaill).ToList();

            foreach (var recipientEmail in recipients)
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.EMail);
                email.To.Add(MailboxAddress.Parse(recipientEmail));
                email.Subject = "HELLO";
                var builder = new BodyBuilder();
                builder.HtmlBody = "<html><body><h1>YuvaPrasath</h1></body></html>";
                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.EMail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
        }
    }
}
