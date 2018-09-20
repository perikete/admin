using System.ComponentModel.DataAnnotations;

namespace Admin.Api.Models.Account {
    public class CredentialsViewModel {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}