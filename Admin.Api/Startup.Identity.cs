using System;
using System.Text;
using Admin.Api.Core.Extensions;
using Admin.Api.Data.DataContexts;
using Admin.Api.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Admin.Api
{
    public partial class Startup
    {
        private void ConfigureJwt (IServiceCollection services)
        {
            // Add framework services.
            services.AddTransient<SignInManager<User>> ();
            services.AddTransient<UserManager<User>> ();
            services.AddTransient<IHttpContextAccessor,HttpContextAccessor>();

            var signingKey = new SymmetricSecurityKey (Encoding.ASCII.GetBytes (Configuration.GetKey ()));

            var tokenValidationParameters = new TokenValidationParameters
            {
                RequireExpirationTime = true,
                RequireSignedTokens = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = Configuration.GetIssuer (),
                ValidateAudience = true,
                ValidAudience = Configuration.GetAudience (),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication (options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer (options =>
            {
                options.Audience = Configuration.GetAudience ();
                options.ClaimsIssuer = Configuration.GetIssuer ();
                options.TokenValidationParameters = tokenValidationParameters;
                options.SaveToken = true;
            });

            services.AddIdentityCore<User>()
                .AddEntityFrameworkStores<AdminDataContext> ()
                .AddDefaultTokenProviders ();
        }
    }
}