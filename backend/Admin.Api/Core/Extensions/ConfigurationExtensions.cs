using Microsoft.Extensions.Configuration;

namespace Admin.Api.Core.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetIssuer (this IConfiguration config)
        {
            return config["Tokens:Issuer"];
        }

        public static string GetAudience (this IConfiguration config)
        {
            return config["Tokens:Audience"];
        }

        public static string GetKey (this IConfiguration config)
        {
            return config["Tokens:Key"];
        }

        public static string GetCorsOrigin (this IConfiguration config)
        {
            return config["CorsOrigin"];
        }

    }
}