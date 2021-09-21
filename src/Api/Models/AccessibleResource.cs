using System;
using System.Linq;
using Api.AccessControl;

namespace Api.Models
{
    public abstract class AccessibleResource : IAccessibleResource
    {
        public long Id { get; set; }
        public long BudgetId { get; set; }

        public virtual bool IsOwnedBy(Guid principalId)
        {
            var strPrincipalId = principalId.ToString();
            bool exists = false;
            using (var db = new BudgetContext())
            {
                exists = db.ResourceUsers.Any(
                    ru => ru.BudgetId == this.BudgetId &&
                    ru.PrincipalId == strPrincipalId &&
                    ru.ResourceType == this.GetType().Name &&
                    ru.ResourceId == this.Id
                    );
            }
            return exists;
        }

        public virtual bool IsParentOwnedBy(Guid principalId)
        {
            return this.IsOwnedBy(principalId);
        }

        public abstract TResponseModel ToResponseModel<TResponseModel>(IAccessibleResource accessibleResource);
    }
}
