using EmailTracker.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailTracker.Service.IServices
{
    public interface IEmailService
    {
        Task SendEmail(Email email);
        Task DeleteEmail(int emailId);
        Task UndeleteEmail(int emailId);
        Task<IEnumerable<Email>> GetAllEmails();
        Task<Email> GetEmailById(int emailId);
        Task<IEnumerable<Email>> FilterEmailsByLabelNameArchivedStatusOrFromEmailAddress(string labelName, bool? isArchived, string fromAddress);
    }
}
