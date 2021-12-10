using System.Threading.Tasks;

namespace EmailTracker.Repository.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task Add(T entity);
        Task Delete(int id);
    }
}
