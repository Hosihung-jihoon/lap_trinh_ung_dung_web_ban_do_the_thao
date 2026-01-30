using System.Threading.Tasks;

namespace ECommerce.Application.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
        Task SendPasswordResetEmailAsync(string toEmail, string resetToken);
        Task SendWelcomeEmailAsync(string toEmail, string fullName);
    }
}