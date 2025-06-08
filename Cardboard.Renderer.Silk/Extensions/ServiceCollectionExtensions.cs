using Cardboard.Core.Interfaces;
using Cardboard.Renderer.Silk.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Cardboard.Renderer.Silk.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection WithSilkRenderer(this IServiceCollection services)
        {
            services.AddSingleton<IRenderer, SilkRenderer>();
            services.AddSingleton<IWindowManager, SilkWindowManager>();
            services.AddSingleton<IElementRendererFactory, SilkElementRendererFactory>();

            return services;
        }
    }
}