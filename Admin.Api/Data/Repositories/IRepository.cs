using System.Collections.Generic;
using System.Threading.Tasks;

namespace Admin.Api.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
         Task AddAsync(T entity);
         IEnumerable<T> GetAll();
    }
}