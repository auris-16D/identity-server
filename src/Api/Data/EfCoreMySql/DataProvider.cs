using System;
using System.Collections.Generic;
using System.Linq;
using Api.Controllers.PrincipleApi;
using Api.Models;

namespace Api.Data.EfCoreMySql
{
    public class DataProvider
    {
        public DataProvider()
        { }

        public bool CreatePrinciple(Principle principle)
        {
            var saved = -1;
            using(var db = new BudgetContext())
            {
                db.Principles.Add(principle);
                saved = db.SaveChanges();
            }
            return saved > 0;
        }

        public List<Budget> GetAllBudgetsForPrinciple(Guid principleId)
        {
            var principleIdStr = principleId.ToString();
            var budgets = new List<Budget>();
            var strPrincipleId = principleId.ToString();
            var budgetType = new Budget().GetType().Name;
            using (var dbContext = new BudgetContext())
            {
                budgets = (from b in dbContext.Budgets
                           join ru in dbContext.ResourceUsers
                           on b.BudgetId equals ru.BudgetId
                           where ru.PrincipleId == strPrincipleId &&
                           ru.ResourceType == budgetType
                           select (new Budget
                           {
                               BudgetId = b.BudgetId,
                               Description = b.Description,
                               CreatedAt = b.CreatedAt,
                               UpdatedAt = b.UpdatedAt,

                               Accounts = b.Accounts,
                               Categories = b.Categories,
                               Contacts = b.Contacts,
                               GroupCategories = b.GroupCategories,
                               Groups = b.Groups,
                               PrincipleResourcePolicies = b.PrincipleResourcePolicies,
                               Reconciliations = b.Reconciliations,
                               ResourcePolicies = b.ResourcePolicies,
                               ResourceUsers = b.ResourceUsers,
                               TransactionHeaders = b.TransactionHeaders,
                               TransactionItems = b.TransactionItems
                           })).ToList();
            }
            return budgets;
        }

        public Budget GetBudgetByIdForPrinciple(Guid principleId, long budgetId)
        {
            var budget = new Budget();
            var strPrincipleId = principleId.ToString();
            var budgetType = new Budget().GetType().Name;
            using (var dbContext = new BudgetContext())
            {
                budget = (from b in dbContext.Budgets
                           join ru in dbContext.ResourceUsers
                           on b.BudgetId equals ru.BudgetId
                           where ru.PrincipleId == strPrincipleId &&
                           ru.ResourceType == budgetType &&
                           ru.ResourceId == budgetId
                          select (new Budget
                          {
                              BudgetId = b.BudgetId,
                              Description = b.Description,
                              CreatedAt = b.CreatedAt,
                              UpdatedAt = b.UpdatedAt,

                              Accounts = b.Accounts,
                              Categories = b.Categories,
                              Contacts = b.Contacts,
                              GroupCategories = b.GroupCategories,
                              Groups = b.Groups,
                              PrincipleResourcePolicies = b.PrincipleResourcePolicies,
                              Reconciliations = b.Reconciliations,
                              ResourcePolicies = b.ResourcePolicies,
                              ResourceUsers = b.ResourceUsers,
                              TransactionHeaders = b.TransactionHeaders,
                              TransactionItems = b.TransactionItems
                          })).ToList().FirstOrDefault();
            }

            return budget;
        }
    }
}
