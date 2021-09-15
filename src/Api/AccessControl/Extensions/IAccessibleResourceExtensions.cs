using System.Linq;
using Api.Library.Extensions;
using Api.Models;

namespace Api.AccessControl.Extensions
{
    public enum CrudAction
    {
        Create,
        Read,
        Update,
        Delete,
        Full
    };

    public static class IAccessibleResourceExtensions
    {
        public static bool CanCreate(this IAccessibleResource accessibleResource, string principleId)
        {
            if(!accessibleResource.IsParentOwnedBy(principleId.ToGuid()))
            {
                return false;
            }
            return CanAccess(accessibleResource, principleId, CrudAction.Create);
        }

        public static bool CanRead(this IAccessibleResource accessibleResource, string principleId)
        {
            return CanAccess(accessibleResource, principleId, CrudAction.Read);
        }

        public static bool CanUpdate(this IAccessibleResource accessibleResource, string principleId)
        {
            return CanAccess(accessibleResource, principleId, CrudAction.Update);
        }

        public static bool CanDelete(this IAccessibleResource accessibleResource, string principleId)
        {
            return CanAccess(accessibleResource, principleId, CrudAction.Delete);
        }

        private static bool CanAccess(IAccessibleResource accessibleResource, string principleId, CrudAction crudAction)
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
