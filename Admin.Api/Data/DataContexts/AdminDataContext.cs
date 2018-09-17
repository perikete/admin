using Admin.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Admin.Api.Data.DataContexts
{
    public class AdminDataContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public AdminDataContext(DbContextOptions<AdminDataContext> options)
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
        }
    }
}