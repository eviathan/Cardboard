using Microsoft.Extensions.DependencyInjection;
using Cardboard.Engine;
using Cardboard.Renderer.Silk.Extensions;
using Cardboard.Templating.CSX.Extensions;
using Cardboard.Sandbox.Components;
using Cardboard.Renderer.Engine.Extensions;

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
                    services.AddCardboard();
                    services.AddSilkRenderer();
                    services.AddCardboardCSXTemplator();
                })
                .Build();

            host.Run(
                (host) =>
                {
                    Console.WriteLine("App starting...");
                    var window = host.WindowManager.CreateWindow(string.Empty, 1200, 800);
                    host.App.SetRootComponent<RootComponent>(window);
                },
                (host) =>
                {
                    Console.WriteLine("App stopping...");
                }
            );
        }
    }
}