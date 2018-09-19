using System.ComponentModel.DataAnnotations;
using Admin.Api.Data.Entities;

namespace Admin.Api.Models.Customer
{
    public class ChangeStatusModel
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public StatusEnum NewStatus { get; set; }
    }
}