using Api.Models;

namespace Api.Data.Interfaces.Commands
{
    public interface ICreatePrincipalCommand
    {
        /// <summary>
        /// Creates a Principal record
        /// </summary>
        /// <param name="principal"></param>
        /// <returns>bool</returns>
        bool CreatePrincipal (Principal principal);
    }
}
