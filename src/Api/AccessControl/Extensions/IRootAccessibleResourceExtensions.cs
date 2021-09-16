using System.Linq;
using Api.Library.Extensions;
using Api.Models;
using Api.Library.Enums;

namespace Api.AccessControl.Extensions
{
    public static class IRootAccessibleResourceExtensions
    {
        public static bool CanCreate(this IRootAccessibleResource accessibleResource, string principleId)
        {
            if(!accessibleResource.IsOwnedBy(principleId.ToGuid()))
            {
                return false;
            }
            return CanAccess(accessibleResource, principleId, CrudAction.Create);
        }

        public static bool CanRead(this IRootAccessibleResource accessibleResource, string principleId)
        {
            return CanAccess(accessibleResource, principleId, CrudAction.Read);
        }

        public static bool CanUpdate(this IRootAccessibleResource accessibleResource, string principleId)
        {
            return CanAccess(accessibleResource, principleId, CrudAction.Update);
        }

        public static bool CanDelete(this IRootAccessibleResource accessibleResource, string principleId)
        {
            return CanAccess(accessibleResource, principleId, CrudAction.Delete);
        }

        private static bool CanAccess(IRootAccessibleResource accessibleResource, string principleId, CrudAction crudAction)
        {
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
                    var principlePolicy = db.PrincipleResourcePolicies.Any(prp =>
                    prp.PrincipleGuid == principleId &&
                    prp.BudgetId == accessibleResource.BudgetId &&
                    prp.ResourcePolicyId == policy.Id
                    );
                    isaccessable = principlePolicy;
                }
            }
            return isaccessable;
        }
    }
}
