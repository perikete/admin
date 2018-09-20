using System.ComponentModel.DataAnnotations;

namespace Admin.Api.Models.Customer
{
    public class AddNoteModel
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public NoteModel Note { get; set; }
    }
}