using Cardboard.Core.Interfaces;
using Cardboard.Core.Models;

namespace Cardboard.Renderer.Silk
{
    public class SilkRenderer : IRenderer
    {
        private readonly IElementRendererFactory _elementRendererFactory;

        public SilkRenderer(IElementRendererFactory elementRendererFactory)
        {
            _elementRendererFactory = elementRendererFactory ?? throw new ArgumentNullException(nameof(elementRendererFactory));
        }

        public void Initialise(IEnumerable<IRenderableElement> elements, nint nativeWindowHandle)
        {
            foreach (var element in elements)
            {
                var renderer = _elementRendererFactory.GetElementRenderer(element);
                renderer.Initialise(element);
            }
        }

        public void Render(IEnumerable<IRenderableElement> elements, double delta)
        {
            foreach (var element in elements)
            {
                var renderer = _elementRendererFactory.GetElementRenderer(element);
                renderer.Render(element);
            }
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