using System;
using System.Collections.Generic;

#nullable disable

namespace Api.Models
{
    public partial class TransactionHeader
    {
        public TransactionHeader()
        {
            TransactionItems = new HashSet<TransactionItem>();
        }

        public long Id { get; set; }
        public long BudgetId { get; set; }
        public string PrincipleGuid { get; set; }
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
        public virtual Reconciled ReconciledNavigation { get; set; }
        public virtual ICollection<TransactionItem> TransactionItems { get; set; }
    }
}
