using Admin.Api.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Admin.Api.Data.DataContexts
{
    public class AdminDataContext : IdentityDbContext<User>
    {
        public DbSet<Customer> Customers { get; set; }
        
        public AdminDataContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Customer>().HasMany(o => o.Notes);

            modelBuilder.Entity<Note>()
                .HasKey(o => o.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}