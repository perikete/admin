using Microsoft.AspNetCore.Identity;

namespace Admin.Api.Data.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}