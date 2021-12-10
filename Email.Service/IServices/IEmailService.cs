using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailTracker.Service.IServices
{
    public interface IEmailService
    {
        Task SendEmail(Core.Models.Email email);
        Task DeleteEmail(int emailId);
        Task<IEnumerable<Core.Models.Email>> GetAllEmailsBySenderEmailAddress(string senderEmailAddres);
    }
}
