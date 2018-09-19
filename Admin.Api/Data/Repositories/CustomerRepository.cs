using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.Api.Data.DataContexts;
using Admin.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Admin.Api.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
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

        public async Task<IEnumerable<Customer>> GetAllAsync ()
        {
            return await Customers.Include (o => o.Notes).ToListAsync ();
        }

        public async Task<Customer> GetByIdAsync (int id)
        {
            var all = await GetAllAsync ();
            return all.FirstOrDefault (o => o.Id == id);
        }

        public async Task DeleteAsync (Customer entity)
        {
            Customers.Remove (entity);

            await _context.SaveChangesAsync ();
        }

        public async Task AddNoteAsync (Customer customer, Note newNote)
        {
            customer.Notes.Add (newNote);

            await _context.SaveChangesAsync ();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}