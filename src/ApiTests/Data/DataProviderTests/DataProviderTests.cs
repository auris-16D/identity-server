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
                db.Principles.Add(new Principle
                {
                    Id = principleId.ToString()
                });
                var budget = new Budget();
                db.Budgets.Add(budget);
                db.SaveChanges();

                var resourceUser = new ResourceUser
                {
                    PrincipleId = principleId.ToString(),
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
    }
}
