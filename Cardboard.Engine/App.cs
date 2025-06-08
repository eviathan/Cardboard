using System.ComponentModel;
using Cardboard.Core.Interfaces;

using IComponent = Cardboard.Core.Interfaces.IComponent;

namespace Cardboard.Engine
{
    public class App : IApp
    {
        private readonly ITreeManager _treeManager;
        private readonly IRenderer _renderer;
        private IWindow? _window;
        private IElement? _rootComponent;

        public App(ITreeManager treeManager, IRenderer renderer)
        {
            _treeManager = treeManager ?? throw new ArgumentNullException(nameof(treeManager));
            _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
        }

        public void SetRootComponent<TComponent>(IWindow window) where TComponent : IElement, new()
        {
            _window = window;
            _rootComponent = new TComponent();
            _treeManager.SetRoot(_rootComponent);
            _window.SetRootComponent(_rootComponent as IComponent);
            _window.Show();

            // Initialize renderer with the native window handle (you'll need to expose this from IWindow)
            _renderer.Initialise(_window.NativeHandle);
        }

        public void Run()
        {
            // while (true)
            // {
            //     // Ideally you also reconcile tree if state changed, etc.
            //     _renderer.Render(_treeManager.RootElement);

            //     // Simple throttle - use a timer or proper event loop instead
            //     System.Threading.Thread.Sleep(16);
            // }
        }

        public void Exit()
        {
            _renderer.Dispose();
            _window?.Close();
        }
    }
}