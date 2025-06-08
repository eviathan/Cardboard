using Cardboard.Core.Interfaces;
using Cardboard.Renderer.Silk.Contexts;
using Cardboard.Renderer.Silk.ElementRenderers;
using Cardboard.Renderer.Silk.Factories;
using Microsoft.Extensions.DependencyInjection;
using Silk.NET.OpenGL;

namespace Cardboard.Renderer.Silk.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection WithSilkRenderer(this IServiceCollection services)
        {
            services.AddSingleton<IRenderer, SilkRenderer>();
            services.AddSingleton<IWindowManager, SilkWindowManager>();
            services.AddSingleton<IElementRendererFactory, SilkElementRendererFactory>();
            services.AddSingleton<IDrawingContext<GL>, GLDrawingContext>();

            // Element Renderers
            services.AddTransient<BoxElementRenderer>();
            services.AddTransient<TextElementRenderer>();
            services.AddTransient<ImageElementRenderer>();
            services.AddTransient<VectorElementRenderer>();

            return services;
        }
    }
}