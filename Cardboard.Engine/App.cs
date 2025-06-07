using System.ComponentModel;
using Cardboard.Core.Interfaces;

using IComponent = Cardboard.Core.Interfaces.IComponent;

namespace Cardboard.Engine
{
    public class App : IApp
    {
        private readonly ITreeManager _treeManager;
        private IWindow? _window;
        private IElement? _rootComponent;

        public App(ITreeManager treeManager)
        {
            _treeManager = treeManager;
        }

        public void SetRootComponent<TComponent>(IWindow window) where TComponent : IElement, new()
        {
            _window = window;
            _rootComponent = new TComponent();
            _treeManager.SetRoot(_rootComponent);
            _window.SetRootComponent(_rootComponent as IComponent);
            _window.Show();
        }

        public void Run()
        {
            // _window?.Show();
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }
    }
}