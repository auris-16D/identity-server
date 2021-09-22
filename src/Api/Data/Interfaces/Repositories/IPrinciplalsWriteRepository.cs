using Api.Models;

namespace Api.Data.Interfaces.Repositories
{
    public interface IPrincipalsWriteRepository
    {
        bool CreatePrincipal(Principal principal);
    }
}
