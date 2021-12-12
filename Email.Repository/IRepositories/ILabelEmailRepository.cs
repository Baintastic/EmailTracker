using EmailTracker.Core;
using System.Threading.Tasks;

namespace EmailTracker.Repository.IRepositories
{
    public interface ILabelEmailRepository
    {
        Task Add(LabelEmail entity);
        Task Delete(int id, int emailId);
    }
}
