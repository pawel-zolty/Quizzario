using System.Threading.Tasks;

namespace Quizzario.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
