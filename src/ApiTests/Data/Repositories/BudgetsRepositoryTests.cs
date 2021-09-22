using System;
using Api.Data.Interfaces.Repositories;
using Api.Models;
using ApiTests.AccessControl;
using NUnit.Framework;

namespace ApiTests.Data.Repostories
{
    [TestFixture]
    public class BudgetsRepositoryTests : TestBase
    {
        private IBudgetsReadRepository budgetsReadRepository;

        public BudgetsRepositoryTests(IBudgetsReadRepository budgetsReadRepository) : base(budgetsReadRepository)
        { }

        [Test]
        public void IsOwner_Success()
        {
            using (var db = new BudgetContext())
            {
                var principalId = Guid.NewGuid();
                db.Principals.Add(new Principal
                {
                    Id = principalId.ToString()
                });
                var budget = new Budget();
                db.Budgets.Add(budget);
                db.SaveChanges();

                var resourceUser = new ResourceUser
                {
                    PrincipalId = principalId.ToString(),
                    ResourceId = budget.BudgetId,
                    ResourceType = budget.GetType().Name,
                    BudgetId = budget.BudgetId
                };

                budget.ResourceUsers.Add(resourceUser);
                db.SaveChanges();

                var accessibleBudgets = this.budgetsReadRepository.GetAllBudgetsForPrincipal(principalId);
                Assert.AreEqual(1, accessibleBudgets.Count);
            }
        }
    }
}
