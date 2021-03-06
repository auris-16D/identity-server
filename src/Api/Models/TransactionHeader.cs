using System;
using System.Collections.Generic;
using Api.AccessControl;

#nullable disable

namespace Api.Models
{
    public partial class TransactionHeader : AccessibleResource
    {
        public TransactionHeader()
        {
            TransactionItems = new HashSet<TransactionItem>();
        }

        public string PrincipalId { get; set; }
        public DateTime TransactionDate { get; set; }
        public long AccountId { get; set; }
        public long ContactId { get; set; }
        public string Sign { get; set; }
        public decimal Total { get; set; }
        public DateTime? Reconciled { get; set; }
        public long ReconciledId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Account Account { get; set; }
        public virtual Budget Budget { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual Principal Principal { get; set; }
        public virtual Reconciliation ReconciledNavigation { get; set; }
        public virtual ICollection<TransactionItem> TransactionItems { get; set; }

        public override bool IsParentOwnedBy(Guid principalId)
        {
            return this.Account.IsOwnedBy(principalId);
        }

        public override TResponseModel ToResponseModel<TResponseModel>(IAccessibleResource accessibleResource)
        {
            throw new NotImplementedException();
        }
    }
}
