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
        public IApp App => _services.GetRequiredService<IApp>();

        private IComponent _rootComponent { get; set; }
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

            var window = WindowManager.CreateWindow(string.Empty, 1200, 800, _rootComponent);
            window.Show();

            // Initialize renderer with the native window handle (you'll need to expose this from IWindow)
            // _renderer.Initialise(window.NativeHandle);

            Console.WriteLine("App stopping...");
            onStop?.Invoke();
        }
    }
}