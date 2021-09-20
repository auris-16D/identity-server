using Api.Data.EfCoreMySql;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers.PrincipleApi
{
    [ApiController]
    [Route("api/principles")]
    public class PrinciplesController : ControllerBase
    {
        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Create(PrincipleRequestModel principleModel)
        {
            bool success = false;
            var principle = principleModel.ToPrinciple();
            try
            {
                success = new DataProvider().CreatePrinciple(principle);
            }
            catch (System.Exception ex)
            {
                if(ex.InnerException != null)
                {
                    return BadRequest(ex.InnerException.Message);
                }
                return BadRequest(ex.Message);
            }
            
            if (success)
            {
                return StatusCode(201, principle.Id);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
