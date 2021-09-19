using System;
using Api.AccessControl.Extensions;
using Api.Models;
using NUnit.Framework;
using Api.Library.Enums;
using Api.AccessControl;

namespace ApiTests.AccessControl.Permissions
{
    public class AccountPermissionTests : TestBase
    {
        [Test]
        public void AccessAccount_Success()
        {
            foreach(var action in Enum.GetValues<CrudAction>())
            {
                var canAccess = false;
                var principleId = Guid.NewGuid();
                var account = SetupAccountAndPermissions(principleId, action);
                canAccess = CanAccessForAction(principleId.ToString(), action, account);
                Assert.IsTrue(canAccess,$"Failed to access Account for Action: {action}");
            } 
        }

        [Test]
        public void AccessAccount_Fail_No_ResourcePolicy()
        {
            foreach (var action in Enum.GetValues<CrudAction>())
            {
                bool canAccess = true;
                Account account;
                var principleId = Guid.NewGuid();
                using (var db = new BudgetContext())
                {
                    var budget = new Budget();
                    db.Budgets.Add(budget);
                    db.SaveChanges();

                    // unsaved account
                    account = new Account(budget);
                    budget.Accounts.Add(account);
                }
                canAccess = CanAccessForAction(principleId.ToString(), action, account);
                Assert.IsFalse(canAccess, $"Access granted to Account for Action: {action} without a Resourcec Policy");
            }
        }

        [Test]
        public void AccessAccount_Fail_No_PrincipleResourcePolicy()
        {
            foreach (var action in Enum.GetValues<CrudAction>())
            {
                bool canAccess = true;
                using (var db = new BudgetContext())
                {
                    var principleId = Guid.NewGuid();
                    var budget = new Budget();
                    db.Budgets.Add(budget);
                    var resourcePolicy = new ResourcePolicy
                    {
                        BudgetId = budget.BudgetId,
                        ResourceName = "Account",
                        ResourceAction = action.ToString(),
                        Description = $"{action} Account"
                    };

                    budget.ResourcePolicies.Add(resourcePolicy);
                    var account = new Account(budget);
                    account.BudgetId = budget.BudgetId;
                    budget.Accounts.Add(account);
                    db.SaveChanges();

                    canAccess = CanAccessForAction(principleId.ToString(), action, account);
                }
                Assert.IsFalse(canAccess);
            }
        }

        private Account SetupAccountAndPermissions(Guid principleId, CrudAction crudAction)
        {
            var budget = new Budget();
            Account account = new Account(budget);
            using (var db = new BudgetContext())
            {
                db.Budgets.Add(budget);
                var resourcePolicy = new ResourcePolicy
                {
                    BudgetId = budget.BudgetId,
                    ResourceName = "Account",
                    ResourceAction = crudAction.ToString(),
                    Description = $"{crudAction} Account"
                };

                var principleResourcePolicy = new PrincipleResourcePolicy
                {
                    PrincipleGuid = principleId.ToString(),
                    BudgetId = budget.BudgetId
                };

                resourcePolicy.PrincipleResourcePolicies.Add(principleResourcePolicy);
                budget.ResourcePolicies.Add(resourcePolicy);
                budget.PrincipleResourcePolicies.Add(principleResourcePolicy);
                db.SaveChanges();
                var resourceUser = new ResourceUser
                {
                    PrincipleGuid = principleId.ToString(),
                    ResourceId = budget.BudgetId,
                    ResourceType = budget.GetType().Name,
                    BudgetId = budget.BudgetId
                };

                budget.ResourceUsers.Add(resourceUser);

                if (crudAction != CrudAction.Create)
                {
                    account = new Account(budget);
                    account.BudgetId = budget.BudgetId;
                    budget.Accounts.Add(account);
                    db.SaveChanges();
                }
                else
                {
                    db.SaveChanges();
                    account = new Account(budget);
                    account.Budget = budget;
                    budget.Accounts.Add(account);
                }
            }
            return account;
        }

        private bool CanAccessForAction(string principleId, CrudAction action, IAccessibleResource resource)
        {
            var canAccess = false;
            switch (action)
            {
                case CrudAction.Create:
                    canAccess = resource.CanCreate(principleId.ToString());
                    break;
                case CrudAction.Read:
                    canAccess = resource.CanRead(principleId.ToString());
                    break;
                case CrudAction.Update:
                    canAccess = resource.CanUpdate(principleId.ToString());
                    break;
                case CrudAction.Delete:
                    canAccess = resource.CanDelete(principleId.ToString());
                    break;
                case CrudAction.Full:
                    canAccess = resource.CanCreate(principleId.ToString());
                    Assert.IsFalse(canAccess, $"Access granted to Account for Action: {CrudAction.Create} without Full permissions");

                    canAccess = resource.CanRead(principleId.ToString());
                    Assert.IsFalse(canAccess, $"Access granted to Account for Action: {CrudAction.Read} without Full permissions");

                    canAccess = resource.CanUpdate(principleId.ToString());
                    Assert.IsFalse(canAccess, $"Access granted to Account for Action: {CrudAction.Update} without Full permissions");

                    canAccess = resource.CanDelete(principleId.ToString());
                    Assert.IsFalse(canAccess, $"Access granted to Account for Action: {CrudAction.Delete} without Full permissions");
                    break;
            }
            return canAccess;
        }
    }
}
