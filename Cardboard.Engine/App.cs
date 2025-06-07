using Cardboard.Core.Interfaces;

namespace Cardboard.Engine
{
    public class App : IApp
    {
        private IWindow? _window;
        private IElement? _rootComponent;

        public void SetRootComponent<TComponent>(IWindow window) where TComponent : IElement, new()
        {
            _window = window;
            _rootComponent = new TComponent();

            // _window.SetContent(_rootComponent);
        }

        public void Run()
        {
            _window?.Show();
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }
    }
}