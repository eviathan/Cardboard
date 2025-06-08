using Cardboard.Core.Interfaces;

namespace Cardboard.Renderer.Silk
{
    public class SilkWindowManager : IWindowManager
    {
        private readonly IRenderer _renderer;

        public SilkWindowManager(IRenderer renderer)
        {
            _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
        }

        public IWindow CreateWindow(string title, int width, int height)
        {
            return new SilkWindow(title, width, height, _renderer);
        }
    }
}