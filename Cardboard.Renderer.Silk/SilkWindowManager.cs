using Cardboard.Core.Interfaces;

namespace Cardboard.Renderer.Silk
{
    public class SilkWindowManager : IWindowManager
    {
        private readonly IRenderer _renderer;
        private readonly ILayoutManager _layoutManager;

        private Dictionary<Guid, IWindow> _windows { get; set; } = [];

        public SilkWindowManager(IRenderer renderer, ILayoutManager layoutManager)
        {
            _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            _layoutManager = layoutManager ?? throw new ArgumentNullException(nameof(layoutManager));
        }

        public IEnumerable<IWindow> GetAll()
        {
            return _windows.Values;
        }

        public IWindow? Get(Guid id)
        {
            return _windows.TryGetValue(id, out var window)
                ? window 
                : null;
        }

        public IWindow CreateWindow(string title, int width, int height, IComponent rootComponent)
        {
            var window = new SilkWindow(title, width, height, _renderer, _layoutManager);

            window.SetRootComponent(rootComponent);
            _windows.Add(window.Id, window);
            
            return window;
        }
    }
}