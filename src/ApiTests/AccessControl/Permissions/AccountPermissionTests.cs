using System;
using Api.AccessControl.Extensions;
using Api.Models;
using NUnit.Framework;
using Api.Library.Enums;

namespace ApiTests.AccessControl.Permissions
{
    public class AccountPermissionTests : TestBase
    {
        [Test]
        public void CreateAccount_Success()
        {
            var principleId = Guid.NewGuid();
            var account = SetupAccountAndPermissions(principleId, CrudAction.Create);
            var canCreate = account.CanCreate(principleId.ToString());
            Assert.IsTrue(canCreate);
        }

        [Test]
        public void ReadAccount_Success()
        {
            var principleId = Guid.NewGuid();
            var account = SetupAccountAndPermissions(principleId, CrudAction.Read);
            var canRead = account.CanRead(principleId.ToString());
            Assert.IsTrue(canRead);
        }

        [Test]
        public void UpdateAccount_Success()
        {
            var principleId = Guid.NewGuid();
            var account = SetupAccountAndPermissions(principleId, CrudAction.Update);

            var canUpdate = account.CanUpdate(principleId.ToString());
            Assert.IsTrue(canUpdate);
        }

        [Test]
        public void DeleteAccount_Success()
        {
            var principleId = Guid.NewGuid();
            var account = SetupAccountAndPermissions(principleId, CrudAction.Delete);

            var canDelete = account.CanDelete(principleId.ToString());
            Assert.IsTrue(canDelete);
        }

        [Test]
        public void CreateAccount_Fail_No_ResourcePolicy()
        {
            bool canCreate = true;
            using (var db = new BudgetContext())
            {
                var principleId = Guid.NewGuid();
                var budget = new Budget();
                db.Budgets.Add(budget);
                db.SaveChanges();

                // unsaved account
                var account = new Account(budget);
                //account.Budget = budget;
                budget.Accounts.Add(account);

                canCreate = account.CanCreate(principleId.ToString());
            }
            Assert.IsFalse(canCreate);
        }

       

        [Test]
        public void ReadAccount_Fail_No_PrincipleResourcePolicy()
        {
            bool canRead = true;
            using (var db = new BudgetContext())
            {
                var principleId = Guid.NewGuid();
                var budget = new Budget();
                db.Budgets.Add(budget);
                var resourcePolicy = new ResourcePolicy
                {
                    BudgetId = budget.BudgetId,
                    ResourceName = "Account",
                    ResourceAction = CrudAction.Read.ToString(),
                    Description = "Read Account"
                };

                budget.ResourcePolicies.Add(resourcePolicy);
                var account = new Account(budget);
                account.BudgetId = budget.BudgetId;
                budget.Accounts.Add(account);
                db.SaveChanges();

                canRead = account.CanRead(principleId.ToString());
            }
            Assert.IsFalse(canRead);
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
    }
}
