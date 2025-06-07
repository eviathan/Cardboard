using Microsoft.Extensions.DependencyInjection;
using Cardboard.Core.Interfaces;

namespace Cardboard.Engine
{
    public class AppHost
    {
        private readonly IServiceProvider _services;

        public IWindowManager WindowManager => _services.GetRequiredService<IWindowManager>();
        public IRenderer Renderer => _services.GetRequiredService<IRenderer>();
        public IApp App => _services.GetRequiredService<IApp>();

        public AppHost(IServiceProvider services)
        {
            _services = services;
        }

        public void Run(Action<AppHost> onStart, Action<AppHost> onStop)
        {
            onStart(this);

            // Basic loop or delegate to app.Run()
            if (_services.GetService<IApp>() is { } app)
            {
                app.Run();
            }

            onStop(this);
        }

        public static AppHostBuilder CreateBuilder() => new();
    }

    // public static class AppHost
    // {
    //     // public static void Configure(IServiceProvider)

    //     public static void Run<TComponent, TProperties>()
    //         where TComponent : Component<TProperties>, new()
    //         where TProperties : class, new()
    //     {

    //     }
    // }
}