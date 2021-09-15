using System;
using System.Collections.Generic;

#nullable disable

namespace Api.Models
{
    public partial class Reconciled
    {
        public Reconciled()
        {
            TransactionHeaders = new HashSet<TransactionHeader>();
        }

        public long Id { get; set; }
        public long BudgetId { get; set; }
        public string PrincipleGuid { get; set; }
        public decimal ReconciledBalance { get; set; }
        public DateTime ReconciledDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Budget Budget { get; set; }
        public virtual ICollection<TransactionHeader> TransactionHeaders { get; set; }
    }
}
