using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Api.Data.DataContexts;
using Admin.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Admin.Api.Data.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private AdminDataContext _context;
        private DbSet<Customer> Customers => _context.Customers;

        public CustomerRepository (AdminDataContext context)
        {
            _context = context;
        }

        public async Task AddAsync (Customer newCustomer)
        {
            Customers.Add (newCustomer);

            await _context.SaveChangesAsync ();
        }

        public IEnumerable<Customer> GetAll ()
        {
            return Customers;
        }

    }
}