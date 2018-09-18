using System.ComponentModel.DataAnnotations;

namespace Admin.Api.Models.Customer
{
    public class CustomerModel
    {
        [Required]
        public string Fullname { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }
    }
}