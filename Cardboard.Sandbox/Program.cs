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
                    services.AddCardboard();
                    services.AddSilkRenderer();
                    services.AddCardboardCSXTemplating();
                })
                .Build();

            host.Run(
                (options) =>
                {
                    options.Title = "Woooterpooter!";
                    options.Width = 900;
                    options.Height = 900;

                    options.SetRootComponent<RootComponent>();
                }
            );
        }
    }
}