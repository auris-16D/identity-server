using System;
using System.Collections.Generic;
using Api.AccessControl;

#nullable disable

namespace Api.Models
{
    public partial class ContactType : AccessibleResource
    {
        public ContactType()
        {
            Contacts = new HashSet<Contact>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }

        public override TResponseModel ToResponseModel<TResponseModel>(IAccessibleResource accessibleResource)
        {
            throw new NotImplementedException();
        }
    }
}
