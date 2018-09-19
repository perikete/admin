using System.Collections.Generic;
using System.Threading.Tasks;

namespace Admin.Api.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
         Task AddAsync(T entity);
         Task<IEnumerable<T>> GetAllAsync();
         Task<T> GetByIdAsync(int id);
         Task DeleteAsync(T entity);
    }
}