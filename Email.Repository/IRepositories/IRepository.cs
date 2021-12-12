using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailTracker.Repository.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task Add(T entity);
        Task Delete(int id);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
    }
}
