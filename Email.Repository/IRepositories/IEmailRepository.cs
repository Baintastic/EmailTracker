using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailTracker.Repository.IRepositories
{
    public interface IEmailRepository : IGenericRepository<Core.Models.Email>
    {
        Task<IEnumerable<Core.Models.Email>> GetAllEmailsByEmailAddress(string senderEmailAddress);
    }
}
