using System;
using Api.Models;
using NUnit.Framework;

namespace ApiTests.AccessControl.Ownership
{
    [TestFixture]
    public class AccountOwnershipTests : TestBase
    {
        [Test]
        public void IsOwner_Success()
        {
            bool isOwner = false;
            using(var db = new BudgetContext())
            {
                var principleId = Guid.NewGuid();
                var budget = new Budget();
                var account = new Account(budget);
                
                budget.Accounts.Add(account);
                db.Budgets.Add(budget);

                db.SaveChanges();

                var resourceUser = new ResourceUser
                {
                    PrincipleGuid = principleId.ToString(),
                    ResourceId = account.Id,
                    ResourceType = account.GetType().Name,
                    BudgetId = budget.BudgetId
                };

                db.ResourceUsers.Add(resourceUser);
                db.SaveChanges();
                isOwner = account.IsOwnedBy(principleId);
            }
            Assert.IsTrue(isOwner);
        }

        [Test]
        public void IsOwner_Failure_NoResourceUser()
        {
            bool isOwner = true;
            using (var db = new BudgetContext())
            {
                var principleId = Guid.NewGuid();
                var budget = new Budget();
                var account = new Account(budget);

                budget.Accounts.Add(account);
                db.Budgets.Add(budget);

                db.SaveChanges();

                var resourceUser = new ResourceUser
                {
                    PrincipleGuid = Guid.NewGuid().ToString(),
                    ResourceId = account.Id,
                    ResourceType = account.GetType().Name,
                    BudgetId = budget.BudgetId
                };

                db.ResourceUsers.Add(resourceUser);
                db.SaveChanges();
                isOwner = account.IsOwnedBy(principleId);
            }
            Assert.IsFalse(isOwner);
        }

        [Test]
        public void IsOwner_Failure_NoBudget()
        {
            bool isOwner = true;
            using (var db = new BudgetContext())
            {
                var principleId = Guid.NewGuid();
                var budget = new Budget();
                var account = new Account(budget);
                
                var budget2 = new Budget();
                budget.Accounts.Add(account);
                db.Budgets.Add(budget);
                db.Budgets.Add(budget2);

                db.SaveChanges();

                var resourceUser = new ResourceUser
                {
                    PrincipleGuid = Guid.NewGuid().ToString(),
                    ResourceId = account.Id,
                    ResourceType = account.GetType().Name,
                    BudgetId = budget2.BudgetId
                };

                db.ResourceUsers.Add(resourceUser);
                db.SaveChanges();
                isOwner = account.IsOwnedBy(principleId);
            }
            Assert.IsFalse(isOwner);
        }

        [Test]
        public void IsOwner_Failure_IncorrectType()
        {
            bool isOwner = true;
            using (var db = new BudgetContext())
            {
                var principleId = Guid.NewGuid();
                var budget = new Budget();
                var account = new Account(budget);
                
                var budget2 = new Budget();
                budget.Accounts.Add(account);
                db.Budgets.Add(budget);
                db.Budgets.Add(budget2);

                db.SaveChanges();

                var resourceUser = new ResourceUser
                {
                    PrincipleGuid = Guid.NewGuid().ToString(),
                    ResourceId = account.Id,
                    ResourceType = budget.GetType().Name,
                    BudgetId = budget2.BudgetId
                };

                db.ResourceUsers.Add(resourceUser);
                db.SaveChanges();
                isOwner = account.IsOwnedBy(principleId);
            }
            Assert.IsFalse(isOwner);
        }
    }
}
