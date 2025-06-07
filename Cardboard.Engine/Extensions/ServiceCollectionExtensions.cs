using Cardboard.Core.Interfaces;
using Cardboard.Core.Managers;
using Cardboard.Engine;
using Microsoft.Extensions.DependencyInjection;

namespace Cardboard.Renderer.Engine.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCardboard(this IServiceCollection services)
        {
            services.AddSingleton<IApp, App>();
            services.AddSingleton<ITreeManager, TreeManager>();

            return services;
        }
    }
}