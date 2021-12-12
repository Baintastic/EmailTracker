using EmailTracker.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailTracker.Repository.IRepositories
{
    public interface IEmailRepository : IRepository<Email>
    {
        Task UndeleteEmailById(int id);
        Task<IEnumerable<Email>> FilterEmailsByLabelNameArchivedStatusOrFromEmailAddress(string labelName, bool? isArchived, string fromAddress);
    }
}
