using System.Collections.Generic;
using Admin.Api.Data.DataContexts;
using Admin.Api.Model;

namespace Admin.Api.Data.Repositories
{
    public class CustomerRepository
    {
        private AdminDataContext _context;

        public CustomerRepository(AdminDataContext context)
        {
            _context = context;
        }

        public async void AddCustomer(Customer newCustomer)
        {
            _context.Customers.Add(newCustomer);

            await _context.SaveChangesAsync();
        }

        public IEnumerable<Customer> GetAllCustomers() 
        {
            return _context.Customers;
        }

    }
}