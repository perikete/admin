using System.ComponentModel.DataAnnotations;

namespace Admin.Api.Models.Customer
{
    public class NoteModel
    {
        [Required]
        public string Text {get; set; }
    }
}