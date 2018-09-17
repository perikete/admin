using System.Collections.Generic;
using Admin.Api.Data.DataContexts;
using Admin.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Admin.Api.Data.Repositories {
    public class CustomerRepository {
        private AdminDataContext _context;
        private DbSet<Customer> Customers => _context.Customers;

        public CustomerRepository (AdminDataContext context) {
            _context = context;
        }

        public async void AddCustomer (Customer newCustomer) {
            Customers.Add (newCustomer);

            await _context.SaveChangesAsync ();
        }

        public IEnumerable<Customer> GetAllCustomers () {
            return Customers;
        }

    }
}