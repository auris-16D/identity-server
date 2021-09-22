using System.Linq;
using Api.Library.Extensions;
using Api.Models;
using Api.Library.Enums;
using System;

namespace Api.AccessControl.Extensions
{
    public static class AccessibleResourceExtensions
    {
        public static bool CanCreate(this IAccessibleResource accessibleResource, string principalId)
        {
            if(!accessibleResource.IsParentOwnedBy(principalId.ToGuid()))
            {
                return false;
            }
            return CanAccess(accessibleResource, principalId, CrudAction.Create);
        }

        public static bool CanRead(this IAccessibleResource accessibleResource, string principalId)
        {
            return CanAccess(accessibleResource, principalId, CrudAction.Read);
        }

        public static bool CanUpdate(this IAccessibleResource accessibleResource, string principalId)
        {
            return CanAccess(accessibleResource, principalId, CrudAction.Update);
        }

        public static bool CanDelete(this IAccessibleResource accessibleResource, string principalId)
        {
            return CanAccess(accessibleResource, principalId, CrudAction.Delete);
        }

        private static bool CanAccess(IAccessibleResource accessibleResource, string principalId, CrudAction crudAction)
        {
            if (accessibleResource == null)
            {
                throw new ArgumentNullException();
            }

            bool isaccessable = false;

            using (var db = new BudgetContext())
            {
               var policy = db.ResourcePolicies.Where(pr =>
               pr.BudgetId == accessibleResource.BudgetId &&
               pr.ResourceName == accessibleResource.GetType().Name &&
               (pr.ResourceAction == crudAction.ToString() ||
                pr.ResourceAction == CrudAction.Full.ToString())
               ).FirstOrDefault();

               if(policy != null)
               {
                    var principalPolicy = db.PrincipalResourcePolicies.Any(prp =>
                    prp.PrincipalId == principalId &&
                    prp.BudgetId == accessibleResource.BudgetId &&
                    prp.ResourcePolicyId == policy.Id
                    );
                    isaccessable = principalPolicy;
                }
            }
            return isaccessable;
        }
    }
}
