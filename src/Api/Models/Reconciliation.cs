using System;
using System.Collections.Generic;
using Api.AccessControl;

#nullable disable

namespace Api.Models
{
    public partial class Reconciliation : AccessibleResource
    {
        public Reconciliation()
        {
            TransactionHeaders = new HashSet<TransactionHeader>();
        }

        public string PrincipleId { get; set; }
        public decimal ReconciledBalance { get; set; }
        public DateTime ReconciledDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Budget Budget { get; set; }
        public virtual Principle Principle { get; set; }
        public virtual ICollection<TransactionHeader> TransactionHeaders { get; set; }

        public override TResponseModel ToResponseModel<TResponseModel>(IAccessibleResource accessibleResource)
        {
            throw new NotImplementedException();
        }
    }
}
