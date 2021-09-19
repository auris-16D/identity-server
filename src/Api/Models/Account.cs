using System;
using System.Collections.Generic;

#nullable disable

namespace Api.Models
{
    public partial class Account : AccessibleResource
    {
        private Budget _Budget;

        public Account()
        {
            Groups = new HashSet<Group>();
            TransactionHeaders = new HashSet<TransactionHeader>();
        }

        public Account(Budget budget)
        {
            Groups = new HashSet<Group>();
            TransactionHeaders = new HashSet<TransactionHeader>();
            Budget = budget;
        }

        public string Name { get; set; }
        public string AccountType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<TransactionHeader> TransactionHeaders { get; set; }

        public virtual Budget Budget
        {
            get => _Budget;
            set
            {
                _Budget = value;
                this.BudgetId = _Budget.BudgetId;
            }
        }

        public override bool IsParentOwnedBy(Guid principleId)
        {
            return this.Budget.IsOwnedBy(principleId);
        }
}
}
