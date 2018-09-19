using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Admin.Api.Data.Entities
{
    public class Customer : EntityBase
    {
        public string Fullname { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public StatusEnum Status { get; set; }

        public virtual ICollection<Note> Notes { get; set; }

        /*public Customer()
        {
            Notes = new Collection<Note>();
        }*/
    }
}