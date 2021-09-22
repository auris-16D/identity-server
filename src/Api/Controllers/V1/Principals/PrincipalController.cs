using Api.Controllers.V1.Principals;
using Api.Data.Interfaces.Commands;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers.V1.Principals
{
    [ApiController]
    [Route("api/v1/principals")]
    public class PrinciplesController : ControllerBase
    {
        private ICreatePrincipalCommand createPrincipalCommand;

        public PrinciplesController(ICreatePrincipalCommand createPrincipalCommand)
        {
            this.createPrincipalCommand = createPrincipalCommand;
        }

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult Create(PrincipalRequestModel principalModel)
        {
            bool success = false;
            var principal = principalModel.ToPrincipal();
            try
            {
                success = this.createPrincipalCommand.CreatePrincipal(principal);
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
                return StatusCode(201, principal.Id);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
