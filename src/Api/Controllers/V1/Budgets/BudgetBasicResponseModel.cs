using System;
using Api.AccessControl;
using Api.Models;

namespace Api.Controllers.V1.Budgets
{
    public class BudgetBasicResponseModel
    {
        public BudgetBasicResponseModel()
        { }

        public long BudgetId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public BudgetBasicResponseModel FromModel(IAccessibleResource accessibleResource)
        {
            var budget = accessibleResource as Budget;
            return new BudgetBasicResponseModel
            {
                BudgetId = budget.BudgetId,
                Name = budget.Name,
                Description = budget.Description,
                CreatedAt = budget.CreatedAt,
                UpdatedAt = budget.UpdatedAt
            };
        }
    }
}
