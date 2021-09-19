using System;
using Api.Data.EfCoreMySql;
using Api.Models;
using ApiTests.AccessControl;
using NUnit.Framework;

namespace ApiTests.Data.DataProviderTests
{
    [TestFixture]
    public class DataProviderTests : TestBase
    {
        [Test]
        public void IsOwner_Success()
        {
            using(var db = new BudgetContext())
            {
                var principleId = Guid.NewGuid();
                var budget = new Budget();
                db.Budgets.Add(budget);
                db.SaveChanges();

                var resourceUser = new ResourceUser
                {
                    PrincipleGuid = principleId.ToString(),
                    ResourceId = budget.BudgetId,
                    ResourceType = budget.GetType().Name,
                    BudgetId = budget.BudgetId
                };

                budget.ResourceUsers.Add(resourceUser);
                db.SaveChanges();

                var accessibleBudgets = new DataProvider().GetAllBudgetsForPrinciple(principleId);
                Assert.AreEqual(1, accessibleBudgets.Count);
            }
        }

        //[Test]
        //public void IsOwner_Failure_NoResourceUser()
        //{
        //    bool isOwner = true;
        //    using (var db = new BudgetContext())
        //    {
        //        var principleId = Guid.NewGuid();
        //        var budget = new Budget();
        //        var account = new Account(budget);

        //        budget.Accounts.Add(account);
        //        db.Budgets.Add(budget);

        //        db.SaveChanges();

        //        var resourceUser = new ResourceUser
        //        {
        //            PrincipleGuid = Guid.NewGuid().ToString(),
        //            ResourceId = account.Id,
        //            ResourceType = account.GetType().Name,
        //            BudgetId = budget.BudgetId
        //        };

        //        db.ResourceUsers.Add(resourceUser);
        //        db.SaveChanges();
        //        isOwner = account.IsOwnedBy(principleId);
        //    }
        //    Assert.IsFalse(isOwner);
        //}

        //[Test]
        //public void IsOwner_Failure_NoBudget()
        //{
        //    bool isOwner = true;
        //    using (var db = new BudgetContext())
        //    {
        //        var principleId = Guid.NewGuid();
        //        var budget = new Budget();
        //        var account = new Account(budget);
                
        //        var budget2 = new Budget();
        //        budget.Accounts.Add(account);
        //        db.Budgets.Add(budget);
        //        db.Budgets.Add(budget2);

        //        db.SaveChanges();

        //        var resourceUser = new ResourceUser
        //        {
        //            PrincipleGuid = Guid.NewGuid().ToString(),
        //            ResourceId = account.Id,
        //            ResourceType = account.GetType().Name,
        //            BudgetId = budget2.BudgetId
        //        };

        //        db.ResourceUsers.Add(resourceUser);
        //        db.SaveChanges();
        //        isOwner = account.IsOwnedBy(principleId);
        //    }
        //    Assert.IsFalse(isOwner);
        //}

        //[Test]
        //public void IsOwner_Failure_IncorrectType()
        //{
        //    bool isOwner = true;
        //    using (var db = new BudgetContext())
        //    {
        //        var principleId = Guid.NewGuid();
        //        var budget = new Budget();
        //        var account = new Account(budget);
                
        //        var budget2 = new Budget();
        //        budget.Accounts.Add(account);
        //        db.Budgets.Add(budget);
        //        db.Budgets.Add(budget2);

        //        db.SaveChanges();

        //        var resourceUser = new ResourceUser
        //        {
        //            PrincipleGuid = Guid.NewGuid().ToString(),
        //            ResourceId = account.Id,
        //            ResourceType = budget.GetType().Name,
        //            BudgetId = budget2.BudgetId
        //        };

        //        db.ResourceUsers.Add(resourceUser);
        //        db.SaveChanges();
        //        isOwner = account.IsOwnedBy(principleId);
        //    }
        //    Assert.IsFalse(isOwner);
        //}
    }
}
