using System;
using System.Collections.Generic;
using Api.Data.Interfaces.Repositories;
using Api.Models;
using System.Linq;

namespace Api.Data.Repositories
{
    public class BudgetsReadRepository : IBudgetsReadRepository
    {
        

        public BudgetsReadRepository()
        {
        }

        public List<Budget> GetAllBudgetsForPrincipal(Guid principalId)
        {
            var principalIdStr = principalId.ToString();
            var budgets = new List<Budget>();
            var strPrincipalId = principalId.ToString();
            var budgetType = new Budget().GetType().Name;
            using (var dbContext = new BudgetContext())
            {
                budgets = (from b in dbContext.Budgets
                           join ru in dbContext.ResourceUsers
                           on b.BudgetId equals ru.BudgetId
                           where ru.PrincipalId == strPrincipalId &&
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
                               PrincipalResourcePolicies = b.PrincipalResourcePolicies,
                               Reconciliations = b.Reconciliations,
                               ResourcePolicies = b.ResourcePolicies,
                               ResourceUsers = b.ResourceUsers,
                               TransactionHeaders = b.TransactionHeaders,
                               TransactionItems = b.TransactionItems
                           })).ToList();
            }
            return budgets;
        }

        public Budget GetBudgetByIdForPrincipal(Guid principalId, long budgetId)
        {
            var budget = new Budget();
            var strPrincipalId = principalId.ToString();
            var budgetType = new Budget().GetType().Name;
            using (var dbContext = new BudgetContext())
            {
                budget = (from b in dbContext.Budgets
                          join ru in dbContext.ResourceUsers
                          on b.BudgetId equals ru.BudgetId
                          where ru.PrincipalId == strPrincipalId &&
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
                              PrincipalResourcePolicies = b.PrincipalResourcePolicies,
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
