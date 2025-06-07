using Cardboard.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Cardboard.Templating.CSX.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCardboardCSXTemplator(this IServiceCollection services)
        {
            services.AddSingleton<ITemplator, CSXTemplator>();
            // services.AddSingleton<IWindowManager, SilkWindowManager>();

            return services;
        }
    }
}