using System;
using System.Collections.Generic;
using Api.Data.Interfaces.Queries;
using Api.Data.Interfaces.Repositories;
using Api.Models;

namespace Api.Data.Queries
{
    public class BudgetsReadQuery : IBudgetsReadQuery
    {
        private IBudgetsReadRepository budgetsReadRepository;

        public BudgetsReadQuery(IBudgetsReadRepository budgetsReadRepository)
        {
            this.budgetsReadRepository = budgetsReadRepository;
        }

        public List<Budget> GetAllBudgetsForPrincipal(Guid principalId)
        {
            return this.budgetsReadRepository.GetAllBudgetsForPrincipal(principalId);
        }

        public Budget GetBudgetByIdForPrincipal(Guid principalId, long budgetId)
        {
            return this.budgetsReadRepository.GetBudgetByIdForPrincipal(principalId, budgetId);
        }
    }
}
