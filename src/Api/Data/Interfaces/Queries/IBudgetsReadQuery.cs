using System;
using System.Collections.Generic;
using Api.Models;

namespace Api.Data.Interfaces.Queries
{
    public interface IBudgetsReadQuery
    {

        /// <summary>
        /// Gets a Budget for a given Principal
        /// </summary>
        /// <param name="principalId"></param>
        /// <param name="budgetId"></param>
        /// <returns>Budget Instance</returns>
        Budget GetBudgetByIdForPrincipal(Guid principalId, long budgetId);

        /// <summary>
        /// Gets all Budgets for a given Principal
        /// </summary>
        /// <param name="principalId"></param>
        /// <returns>Collection ofBudgets</returns>
        List<Budget> GetAllBudgetsForPrincipal(Guid principalId);
    }
}