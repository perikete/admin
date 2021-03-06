﻿using Admin.Api.Core.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Admin.Api
{
    public partial class Startup
    {
        public Startup (IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services)
        {
            services.AddCors ();
            services.AddCors (options =>
            {
                options.AddPolicy ("CorsPolicy",
                    builder => builder.WithOrigins (Configuration.GetCorsOrigin ())
                    .AllowAnyMethod ()
                    .AllowAnyHeader ()
                    .AllowCredentials ());
            });
            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_1);
            ConfigureDataAccess (services);
            ConfigureJwt (services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment ())
            {
                app.UseDeveloperExceptionPage ();
                app.UseCors ("CorsPolicy");
            }
            else
            {
                app.UseHsts ();
            }

            app.UseHttpsRedirection ();
            app.UseAuthentication ();
            app.UseMvc ();
        }
    }
}