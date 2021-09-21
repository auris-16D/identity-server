using System;
using System.Collections.Generic;
using Api.AccessControl;

#nullable disable

namespace Api.Models
{
    public partial class TransactionItem : AccessibleResource
    {
        public long CategoryId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public long TransactionHeaderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Budget Budget { get; set; }
        public virtual Category Category { get; set; }
        public virtual TransactionHeader TransactionHeader { get; set; }

        public override bool IsOwnedBy(Guid principalId)
        {
            return this.TransactionHeader.IsOwnedBy(principalId);
        }

        public override TResponseModel ToResponseModel<TResponseModel>(IAccessibleResource accessibleResource)
        {
            throw new NotImplementedException();
        }
    }
}
