using Admin.Api.Data.DataContexts;
using Admin.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Admin.Api.Data.Repositories
{
    public class UserRepository
    {
        private readonly AdminDataContext _context;

        private DbSet<User> Users
        {
            get { return _context.Users; }
        }

        public UserRepository(AdminDataContext context)
        {
            _context = context;
        }

        public async void AddUser(User newUser)
        {
            await Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }
    }
}