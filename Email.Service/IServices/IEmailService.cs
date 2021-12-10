using System.Threading.Tasks;

namespace EmailTracker.Service.IServices
{
    public interface IEmailService
    {
        Task SendEmail(Core.Models.Email email);
        Task DeleteEmail(int emailId);
    }
}
