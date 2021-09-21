using System;
using Api.Data.Commands;
using Api.Data.Interfaces.Repositories;
using Api.Data.Interfaces.Commands;
using Api.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

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
