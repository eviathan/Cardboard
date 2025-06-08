using Microsoft.Extensions.DependencyInjection;
using Cardboard.Core.Interfaces;

namespace Cardboard.Engine
{
    public class AppHost
    {
        private readonly IServiceProvider _services;
        public IWindowManager WindowManager => _services.GetRequiredService<IWindowManager>();
        public ITreeManager TreeManager => _services.GetRequiredService<ITreeManager>();
        public IRenderer Renderer => _services.GetRequiredService<IRenderer>();
        public IApp App => _services.GetRequiredService<IApp>(); // NOTE NOT CURRENTLY IN USE BUT WILL USER FACING APP API

        private IComponent _rootComponent { get; set; } = null!;
        private ApplicationOptions ApplicationOptions { get; } = new();

        public AppHost(IServiceProvider services)
        {
            _services = services;
        }

        public static AppHostBuilder CreateBuilder() => new();
        
        public void Run(Action<ApplicationOptions>? onStart = null, Action? onStop = null)
        {
            Console.WriteLine("App starting...");
            onStart?.Invoke(ApplicationOptions);

            _rootComponent = (Activator.CreateInstance(ApplicationOptions.ComponentType) as IComponent)!;

            TreeManager.SetRoot((IElement)_rootComponent);

            var window = WindowManager.CreateWindow(
                ApplicationOptions.Title,
                ApplicationOptions.Width,
                ApplicationOptions.Height,
                _rootComponent
            );

            window.Show();
            
            onStop?.Invoke();
        }
    }
}