using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Api.AccessControl.Extensions;
using Api.Data.Interfaces.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static Api.Library.Common.ErrorMessage;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


namespace Api.Controllers.V1.Budgets
{
    [ApiController]
    [Route("api/v1/budgets")]
    public class BudgetController : ControllerBase
    {
        private IBudgetsReadQuery budgetsReadQuery;
        private ILogger logger;

        public BudgetController(ILogger logger, IBudgetsReadQuery budgetsReadQuery)
        {
            this.logger = logger;
            this.budgetsReadQuery = budgetsReadQuery;
        }

        [HttpGet]
        [Route("{budgetId}/principle/{principalId}")]
        public IActionResult GetById(Guid principalId, long budgetId)
        {
            
            var budget = this.budgetsReadQuery.GetBudgetByIdForPrincipal(principalId, budgetId);

            if (budget == null)
            {
                return BudgetNotFound(budgetId);
            }

            var canRead = budget.CanRead(principalId.ToString());

            if (!canRead)
            {
                return StatusCode(403,$"Access denied to read Budget: {budgetId}. Principle with Id: {principalId} owns this resource but does not have the correct permissions");
            }

            var responseModel = new BudgetResponseModel().FromModel(budget);
            var result = new JsonResult(responseModel);
            return result;
        }

        // Index
        [HttpGet]
        [Route("budgets")]
        [Route("{principalId}")]
        public IActionResult Get(Guid principalId)
        {
            if(principalId == Guid.Empty)
            {
                return BadRequest(Fields.PrincipalId.IsRequired());
            }

            var authBudgets = new List<IResponseModel>();
            var budgets = this.budgetsReadQuery.GetAllBudgetsForPrincipal(principalId);
            foreach(var budget in budgets)
            {
                if(budget.CanRead(principalId.ToString()))
                {
                    authBudgets.Add(new BudgetResponseModel().FromModel(budget));
                }
            }
            return new JsonResult(authBudgets);
        }

        private IActionResult BudgetNotFound(long budgetId)
        {
            var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(string.Format("No Budget with ID = {0}", budgetId)),
                ReasonPhrase = "Budget ID Not Found"
            };
            return NotFound(resp);
        }
    }
}
