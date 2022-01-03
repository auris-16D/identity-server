using System;
using System.Collections.Generic;
using System.Linq;
using Api.AccessControl;
using Api.Models;

namespace Api.Controllers.V1.Budgets
{
    public class BudgetResponseModel
    {
        public BudgetResponseModel()
        { }

        public long BudgetId { get; set; }
        public string Description { get; set; }
        public List<long> AccountIds { get; set; }
        public Dictionary<long, string> CategoryIds { get; set; }
        public Dictionary<long,string> ContactIds { get; set; }
        public List<long> GroupCategoryIds { get; set; }
        public List<long> GroupIds { get; set; }
        public List<long> Reconciled { get; set; }
        public List<long> ResourcePolicyIds { get; set; }
        public List<long> ResourceUserIds { get; set; }
        public List<long> TransactionHeaderIds { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public BudgetResponseModel FromModel(IAccessibleResource accessibleResource)
        {
            var budget = accessibleResource as Budget;
            return new BudgetResponseModel
            {
                BudgetId = budget.BudgetId,
                Description = budget.Description,
                AccountIds = budget.Accounts.Select(a => a.Id).ToList(),
                CategoryIds = budget.Categories.Select(c => new { c.Id, c.Name }).ToDictionary(x => x.Id, x => x.Name),
                ContactIds = budget.Contacts.Select(co => new { co.Id, co.Name }).ToDictionary(x => x.Id, x => x.Name),
                GroupCategoryIds = budget.GroupCategories.Select(gc => gc.Id).ToList(),
                GroupIds = budget.Groups.Select(g => g.Id).ToList(),
                Reconciled = budget.Reconciliations.Select(r => r.Id).ToList(),
                ResourcePolicyIds = budget.ResourcePolicies.Select(rp => rp.Id).ToList(),
                ResourceUserIds = budget.ResourceUsers.Select(ru => ru.Id).ToList(),
                TransactionHeaderIds = budget.TransactionHeaders.Select(th => th.Id).ToList(),
                CreatedAt = budget.CreatedAt,
                UpdatedAt = budget.UpdatedAt
            };
        }
    }
}
