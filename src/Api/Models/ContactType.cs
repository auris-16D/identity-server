using System;
using System.Collections.Generic;

#nullable disable

namespace Api.Models
{
    public partial class ContactType
    {
        public ContactType()
        {
            Contacts = new HashSet<Contact>();
        }

        public long Id { get; set; }
        public long BudgetId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
