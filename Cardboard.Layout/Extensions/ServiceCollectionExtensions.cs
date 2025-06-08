using Microsoft.Extensions.DependencyInjection;
using Cardboard.Core.Interfaces;
using Cardboard.Layout;

namespace Cardboard.Renderer.Layout.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCardboardLayout(this IServiceCollection services)
        {
            services.AddSingleton<ILayoutManager, LayoutManager>();

            return services;
        }
    }
}