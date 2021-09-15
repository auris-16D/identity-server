using System;
using System.Collections.Generic;
using System.Linq;
using Api.AccessControl;
using Api.Library.Extensions;

#nullable disable

namespace Api.Models
{
    public partial class Account : IAccessibleResource
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

        public long Id { get; set; }
        public long BudgetId { get; set; }
        //public Budget Budget { get; set; }
        public string Name { get; set; }
        public string AccountType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Budget Budget
        {
            get => _Budget;
            set
            {
                _Budget = value;
                this.BudgetId = _Budget.BudgetId;
            }
        }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<TransactionHeader> TransactionHeaders { get; set; }

        public bool IsOwnedBy(Guid principleId)
        {
            var strPrincipleId = principleId.ToString();
            bool exists = false;
            using(var db = new BudgetContext())
            {
                exists = db.ResourceUsers.Any(
                    ru => ru.BudgetId == this.BudgetId &&
                    ru.PrincipleGuid == strPrincipleId &&
                    ru.ResourceType == this.GetType().Name &&
                    ru.ResourceId == this.Id
                    );
            }
            return exists;
        }

        public bool IsParentOwnedBy(Guid principleId)
        {
            var strPrincipleId = principleId.ToString();
            bool exists = false;
            using (var db = new BudgetContext())
            {
                exists = db.ResourceUsers.Any(
                    ru => ru.BudgetId == this.BudgetId &&
                    ru.PrincipleGuid == strPrincipleId &&
                    ru.ResourceType == this.Budget.GetType().Name &&
                    ru.ResourceId == this.BudgetId
                    );
            }
            return exists;
        }
    }
}
