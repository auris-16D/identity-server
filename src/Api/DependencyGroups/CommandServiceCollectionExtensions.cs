using Api.Data.Commands;
using Api.Data.Interfaces.Commands;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CommandServiceCollectionExtensions
    {
        public static IServiceCollection AddCommands(
                this IServiceCollection services)
        {
            services.AddSingleton<ICreatePrincipalCommand, CreatePrincipalCommand>();
            return services;
        }
    }
}
