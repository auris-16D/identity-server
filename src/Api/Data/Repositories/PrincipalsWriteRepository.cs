using Api.Data.Interfaces.Repositories;
using Api.Models;

namespace Api.Data.Repositories
{
    public class PrincipalsWriteRepository : IPrincipalsWriteRepository
    {
        public PrincipalsWriteRepository()
        { }

        public bool CreatePrincipal(Principal principal)
        {
            var saved = -1;
            using (var db = new BudgetContext())
            {
                db.Principals.Add(principal);
                saved = db.SaveChanges();
            }
            return saved > 0;
        }
    }
}
