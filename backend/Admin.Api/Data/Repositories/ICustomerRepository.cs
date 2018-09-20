using System.Threading.Tasks;
using Admin.Api.Data.Entities;

namespace Admin.Api.Data.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task AddNoteAsync(Customer customer, Note newNote);
    }
}