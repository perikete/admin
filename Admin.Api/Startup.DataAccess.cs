using Admin.Api.Data.DataContexts;
using Admin.Api.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Admin.Api
{
    public partial class Startup
    {
        private void ConfigureDataAccess (IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString ("admin");
            services.AddDbContext<AdminDataContext> (options => options.UseSqlite (connection));

            services.AddTransient<UserRepository> ();
            services.AddTransient<CustomerRepository> ();
        }
    }
}