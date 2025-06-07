using Cardboard.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Cardboard.Renderer.Silk.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSilkRenderer(this IServiceCollection services)
        {
            services.AddSingleton<IRenderer, SilkRenderer>();
            services.AddSingleton<IWindowManager, SilkWindowManager>();

            return services;
        }
    }
}