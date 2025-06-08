using Cardboard.Renderer.Engine.Extensions;
using Cardboard.Renderer.Layout.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Cardboard.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCardboard(this IServiceCollection services)
        {
            services.AddCardboardEngine();
            services.AddCardboardLayout();

            return services;
        }
    }
}