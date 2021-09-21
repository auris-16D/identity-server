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
                var principalId = Guid.NewGuid();
                var account = SetupAccountAndPermissions(principalId, action);
                canAccess = CanAccessForAction(principalId.ToString(), action, account, true);
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
                var principalId = Guid.NewGuid();
                using (var db = new BudgetContext())
                {
                    var budget = new Budget();
                    db.Budgets.Add(budget);
                    db.SaveChanges();

                    // unsaved account
                    account = new Account(budget);
                    budget.Accounts.Add(account);
                }
                canAccess = CanAccessForAction(principalId.ToString(), action, account, false);
                Assert.IsFalse(canAccess, $"Access granted to Account for Action: {action} without a Resourcec Policy");
            }
        }

        [Test]
        public void AccessAccount_Fail_No_PrincipalResourcePolicy()
        {
            foreach (var action in Enum.GetValues<CrudAction>())
            {
                bool canAccess = true;
                using (var db = new BudgetContext())
                {
                    var principalId = Guid.NewGuid();
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

                    canAccess = CanAccessForAction(principalId.ToString(), action, account, false);
                }
                Assert.IsFalse(canAccess);
            }
        }

        private Account SetupAccountAndPermissions(Guid principalId, CrudAction crudAction)
        {
            var budget = new Budget();
            Account account = new Account(budget);
            using (var db = new BudgetContext())
            {
                db.Principals.Add(new Principal
                {
                    Id = principalId.ToString()
                });

                db.Budgets.Add(budget);
                var resourcePolicy = new ResourcePolicy
                {
                    BudgetId = budget.BudgetId,
                    ResourceName = "Account",
                    ResourceAction = crudAction.ToString(),
                    Description = $"{crudAction} Account"
                };

                var principalResourcePolicy = new PrincipalResourcePolicy
                {
                    PrincipalId = principalId.ToString(),
                    BudgetId = budget.BudgetId
                };

                resourcePolicy.PrincipalResourcePolicies.Add(principalResourcePolicy);
                budget.ResourcePolicies.Add(resourcePolicy);
                budget.PrincipalResourcePolicies.Add(principalResourcePolicy);
                db.SaveChanges();
                var resourceUser = new ResourceUser
                {
                    PrincipalId = principalId.ToString(),
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

        private bool CanAccessForAction(string principalId, CrudAction action, IAccessibleResource resource, bool expectedResult)
        {
            var canAccess = false;
            switch (action)
            {
                case CrudAction.Create:
                    canAccess = resource.CanCreate(principalId.ToString());
                    break;
                case CrudAction.Read:
                    canAccess = resource.CanRead(principalId.ToString());
                    break;
                case CrudAction.Update:
                    canAccess = resource.CanUpdate(principalId.ToString());
                    break;
                case CrudAction.Delete:
                    canAccess = resource.CanDelete(principalId.ToString());
                    break;
                case CrudAction.Full:
                    canAccess = resource.CanCreate(principalId.ToString());
                    Assert.AreEqual(expectedResult, canAccess, $"CanCreate(): {canAccess} for {resource.GetType().Name} for Action: {CrudAction.Create} using Full permissions");

                    canAccess = resource.CanRead(principalId.ToString());
                    Assert.AreEqual(expectedResult, canAccess, $"CanRead(): {canAccess} for {resource.GetType().Name} for Action: {CrudAction.Read} using Full permissions");

                    canAccess = resource.CanUpdate(principalId.ToString());
                    Assert.AreEqual(expectedResult, canAccess, $"CanUpdate(): {canAccess} for {resource.GetType().Name} for Action: {CrudAction.Update} using Full permissions");

                    canAccess = resource.CanDelete(principalId.ToString());
                    Assert.AreEqual(expectedResult, canAccess, $"CanDelete(): {canAccess} for {resource.GetType().Name} for Action: {CrudAction.Delete} using Full permissions");
                    break;
            }
            return canAccess;
        }
    }
}
