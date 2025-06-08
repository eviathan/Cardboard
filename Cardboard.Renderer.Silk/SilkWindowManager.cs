using Cardboard.Core.Interfaces;

namespace Cardboard.Renderer.Silk
{
    public class SilkWindowManager : IWindowManager
    {
        private readonly IRenderer _renderer;
        private readonly ILayoutManager _layoutManager;

        public SilkWindowManager(IRenderer renderer, ILayoutManager layoutManager)
        {
            _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            _layoutManager = layoutManager ?? throw new ArgumentNullException(nameof(layoutManager));
        }

        public IWindow CreateWindow(string title, int width, int height)
        {
            return new SilkWindow(title, width, height, _renderer, _layoutManager);
        }
    }
}