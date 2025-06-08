using Microsoft.Extensions.DependencyInjection;
using Cardboard.Engine;
using Cardboard.Renderer.Silk.Extensions;
using Cardboard.Templating.CSX.Extensions;
using Cardboard.Sandbox.Components;
using Cardboard.Extensions;

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
                    services
                        .AddCardboard()
                        .WithSilkRenderer()
                        .WithCSXTemplating();
                })
                .Build();

            host.Run(
                (options) =>
                {
                    options.Width = 1280;
                    options.Height = 700;

                    options.SetRootComponent<RootComponent>();
                },
                () =>
                { 
                    Console.WriteLine("App stopping...");
                }
            );
        }
    }
}