using System;
using System.Collections.Generic;
using Api.AccessControl;

#nullable disable

namespace Api.Models
{
    public partial class Contact : AccessibleResource
    {
        public Contact()
        {
            TransactionHeaders = new HashSet<TransactionHeader>();
        }

        public long ContactTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Budget Budget { get; set; }
        public virtual ContactType ContactType { get; set; }
        public virtual ICollection<TransactionHeader> TransactionHeaders { get; set; }

        public override bool IsParentOwnedBy(Guid principalId)
        {
            return this.ContactType.IsOwnedBy(principalId);
        }

        public override TResponseModel ToResponseModel<TResponseModel>(IAccessibleResource accessibleResource)
        {
            throw new NotImplementedException();
        }
    }
}
