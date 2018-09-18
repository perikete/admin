using System.Collections.Generic;

namespace Admin.Api.Data.Entities
{
    public class Customer : EntityBase
    {
        public string Fullname { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public StatusEnum Status { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}