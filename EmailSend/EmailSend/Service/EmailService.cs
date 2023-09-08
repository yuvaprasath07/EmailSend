using EmailSend.Model;
using EmailSend.settings;
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
        private readonly EmaillSettings _mailSettings;
        public EmailService(IOptions<EmaillSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(EmailInfo emailInfo)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.EMail);
            email.To.Add(MailboxAddress.Parse(emailInfo.EmailTo));
            email.Subject = emailInfo.Subject;
            var builder = new BodyBuilder();
            if (emailInfo.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in emailInfo.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = emailInfo.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.EMail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
