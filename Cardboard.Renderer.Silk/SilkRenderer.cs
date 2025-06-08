using Cardboard.Core.Interfaces;
using Cardboard.Core.Models;

namespace Cardboard.Renderer.Silk
{
    public class SilkRenderer : IRenderer
    {
        public void Initialise(nint nativeWindowHandle)
        {
            Console.WriteLine("Renderer Initialised");
        }

        public void Render(IEnumerable<IRenderableElement> elements, double delta)
        {
            Console.WriteLine("Renderer Render");
        }

        public void Resize(IEnumerable<IRenderableElement> elements, Size newSize)
        {
            Console.WriteLine("Renderer Resize");
        }
        
        public void Dispose()
        {
            Console.WriteLine("Disposed");
        }
    }
}