using Api.Data.Interfaces.Queries;
using Api.Data.Queries;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class QueryServiceCollectionExtensions
    {
        public static IServiceCollection AddQueries(
                this IServiceCollection services)
        {
            services.AddSingleton<IBudgetsReadQuery, BudgetsReadQuery>();
            return services;
        }
    }
}
