using System;
using System.Collections.Generic;
using System.Linq;
using Api.AccessControl;

#nullable disable

namespace Api.Models
{
    public partial class Budget : IRootAccessibleResource
    {
        public Budget()
        {
            Accounts = new HashSet<Account>();
            Categories = new HashSet<Category>();
            Contacts = new HashSet<Contact>();
            GroupCategories = new HashSet<GroupCategory>();
            Groups = new HashSet<Group>();
            PrincipleResourcePolicies = new HashSet<PrincipleResourcePolicy>();
            Reconcileds = new HashSet<Reconciled>();
            ResourcePolicies = new HashSet<ResourcePolicy>();
            ResourceUsers = new HashSet<ResourceUser>();
            TransactionHeaders = new HashSet<TransactionHeader>();
            TransactionItems = new HashSet<TransactionItem>();
        }

        public long BudgetId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<GroupCategory> GroupCategories { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<PrincipleResourcePolicy> PrincipleResourcePolicies { get; set; }
        public virtual ICollection<Reconciled> Reconcileds { get; set; }
        public virtual ICollection<ResourcePolicy> ResourcePolicies { get; set; }
        public virtual ICollection<ResourceUser> ResourceUsers { get; set; }
        public virtual ICollection<TransactionHeader> TransactionHeaders { get; set; }
        public virtual ICollection<TransactionItem> TransactionItems { get; set; }

        public bool IsOwnedBy(Guid principleId)
        {
            var strPrincipleId = principleId.ToString();
            bool exists = false;
            using (var db = new BudgetContext())
            {
                exists = db.ResourceUsers.Any(
                    ru => ru.BudgetId == this.BudgetId &&
                    ru.PrincipleGuid == strPrincipleId &&
                    ru.ResourceType == this.GetType().Name &&
                    ru.ResourceId == this.BudgetId
                    );
            }
            return exists;
        }
    }
}
