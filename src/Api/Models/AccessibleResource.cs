using System;
using System.Linq;
using Api.AccessControl;

namespace Api.Models
{
    public abstract class AccessibleResource : IAccessibleResource
    {
        public long Id { get; set; }
        public long BudgetId { get; set; }

        public virtual bool IsOwnedBy(Guid principleId)
        {
            var strPrincipleId = principleId.ToString();
            bool exists = false;
            using (var db = new BudgetContext())
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

        public virtual bool IsParentOwnedBy(Guid principleId)
        {
            return this.IsOwnedBy(principleId);
        }
    }
}
