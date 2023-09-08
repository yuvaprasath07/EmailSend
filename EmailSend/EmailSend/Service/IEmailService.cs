
using EmailSend.Model;

namespace EmailSend.Service
{
    public interface IEmailService
    {
        Task SendEmailAsync();
    }
}
