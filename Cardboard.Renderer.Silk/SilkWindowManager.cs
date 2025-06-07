using Cardboard.Core.Interfaces;

namespace Cardboard.Renderer.Silk
{
    public class SilkWindowManager : IWindowManager
    {
        public IWindow CreateWindow(string title, int width, int height)
        {
            return new SilkWindow(title, width, height);
        }
    }
}