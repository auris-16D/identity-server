using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Api.AccessControl.Extensions;
using Api.Data.EfCoreMySql;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using static Api.Library.Common.ErrorMessage;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


namespace Api.Controllers.BudgetApi
{
    [ApiController]
    [Route("api/budgets")]
    public class BudgetController : ControllerBase
    {
        [HttpGet]
        [Route("{principleId}/budget/{budgetId}")]
        public IActionResult GetById(Guid principleId, long budgetId)
        {
            
            var budget = new DataProvider().GetBudgetByIdForPrinciple(principleId, budgetId);

            if (budget == null)
            {
                return BudgetNotFound(budgetId);
            }

            var canRead = budget.CanRead(principleId.ToString());

            if (!canRead)
            {
                return StatusCode(403,$"Access denied to read Budget: {budgetId}. Principle with Id: {principleId} owns this resource but does not have the correct permissions");
            }

            var responseModel = new BudgetResponseModel().FromModel(budget);
            var result = new JsonResult(responseModel);
            return result;
        }

        // Index
        [HttpGet]
        [Route("budget")]
        [Route("{principleId}")]
        public IActionResult Get(Guid principleId)
        {
            if(principleId == Guid.Empty)
            {
                return BadRequest(Fields.PrincipleId.IsRequired());
            }

            var authBudgets = new List<IResponseModel>();
            var budgets = new DataProvider().GetAllBudgetsForPrinciple(principleId);
            foreach(var budget in budgets)
            {
                if(budget.CanRead(principleId.ToString()))
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
