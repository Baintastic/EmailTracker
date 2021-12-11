using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailTracker.Repository.IRepositories
{
    public interface IEmailRepository : IRepository<Core.Models.Email>
    {
        Task<IEnumerable<Core.Models.Email>> GetAllEmailsByEmailAddress(string senderEmailAddress);
        Task<IEnumerable<Core.Models.Email>> GetAllDeletedEmails();
        Task UndeleteEmailById(int id);
        Task<IEnumerable<Core.Models.Email>> GetAllEmailsByLabelName(string labelName);
    }
}
