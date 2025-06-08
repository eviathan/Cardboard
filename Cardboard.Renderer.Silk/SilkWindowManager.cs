using Cardboard.Core.Interfaces;
using Silk.NET.OpenGL;

namespace Cardboard.Renderer.Silk
{
    public class SilkWindowManager : IWindowManager
    {
        private readonly IRenderer _renderer;
        private readonly ILayoutManager _layoutManager;
        private readonly IDrawingContext<GL> _drawingContext;

        private Dictionary<Guid, IWindow> _windows { get; set; } = [];

        public SilkWindowManager(IRenderer renderer, ILayoutManager layoutManager, IDrawingContext<GL> drawingContext)
        {
            _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            _layoutManager = layoutManager ?? throw new ArgumentNullException(nameof(layoutManager));
            _drawingContext = drawingContext ?? throw new ArgumentNullException(nameof(drawingContext));
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
            var window = new SilkWindow(title, width, height, _renderer, _layoutManager, _drawingContext);

            window.SetRootComponent(rootComponent);
            _windows.Add(window.Id, window);
            
            return window;
        }
    }
}