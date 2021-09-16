using System.Linq;
using Api.Library.Extensions;
using Api.Models;
using Api.Library.Enums;

namespace Api.AccessControl.Extensions
{
    public static class IParentAccessibleResourceExtensions
    {
        public static bool CanCreate(this IParentAccessibleResource accessibleResource, string principleId)
        {
            if(!accessibleResource.Budget.IsOwnedBy(principleId.ToGuid()))
            {
                return false;
            }
            return CanAccess(accessibleResource, principleId, CrudAction.Create);
        }

        public static bool CanRead(this IParentAccessibleResource accessibleResource, string principleId)
        {
            return CanAccess(accessibleResource, principleId, CrudAction.Read);
        }

        public static bool CanUpdate(this IParentAccessibleResource accessibleResource, string principleId)
        {
            return CanAccess(accessibleResource, principleId, CrudAction.Update);
        }

        public static bool CanDelete(this IParentAccessibleResource accessibleResource, string principleId)
        {
            return CanAccess(accessibleResource, principleId, CrudAction.Delete);
        }

        private static bool CanAccess(IParentAccessibleResource accessibleResource, string principleId, CrudAction crudAction)
        {
            bool isaccessable = false;

            using (var db = new BudgetContext())
            {
               var policy = db.ResourcePolicies.Where(pr =>
               pr.BudgetId == accessibleResource.Budget.BudgetId &&
               pr.ResourceName == accessibleResource.GetType().Name &&
               (pr.ResourceAction == crudAction.ToString() ||
                pr.ResourceAction == CrudAction.Full.ToString())
               ).FirstOrDefault();

               if(policy != null)
               {
                    var principlePolicy = db.PrincipleResourcePolicies.Any(prp =>
                    prp.PrincipleGuid == principleId &&
                    prp.BudgetId == accessibleResource.Budget.BudgetId &&
                    prp.ResourcePolicyId == policy.Id
                    );
                    isaccessable = principlePolicy;
                }
            }
            return isaccessable;
        }
    }
}
