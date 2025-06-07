using Microsoft.Extensions.DependencyInjection;
using Cardboard.Engine;
using Cardboard.Renderer.Silk.Extensions;
using Cardboard.Templating.CSX.Extensions;

namespace Cardboard.Sandbox
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = AppHost
                .CreateBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSilkRenderer();
                    services.AddCardboardCSXTemplator();
                })
                .Build();

            var services = new ServiceCollection();

            // 2. Register framework services

            services.AddSilkRenderer();
            // services.AddSingleton<ILayoutManager, DefaultLayoutManager>();
            // services.AddSingleton<IWindowManager, SilkWindowManager>(); // optional if you have one

            // // 3. Register your root component (optional; can be instantiated via Activator if generic)
            // services.AddTransient<RootComponent>();

            // // 4. Build the service provider
            // var provider = services.BuildServiceProvider();

            // // 5. Pass the container to AppHost
            // AppHost.Configure(provider);

            // // 6. Run the app
            // AppHost.Run<RootComponent>();
        }
    }
}