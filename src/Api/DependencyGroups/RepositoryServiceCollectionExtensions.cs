using Api.Data.Interfaces.Repositories;
using Api.Data.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RepositoryServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(
                this IServiceCollection services)
        {
            services.AddSingleton<IPrincipalsWriteRepository, PrincipalsWriteRepository>();
            services.AddSingleton<IBudgetsReadRepository, BudgetsReadRepository>();
            return services;
        }
    }
}
