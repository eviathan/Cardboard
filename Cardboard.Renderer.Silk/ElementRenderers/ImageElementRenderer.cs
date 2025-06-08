using Cardboard.Core.Attributes;
using Cardboard.Core.Interfaces;
using Cardboard.Core.Models.Elements.RenderableElements;
using Silk.NET.OpenGL;

namespace Cardboard.Renderer.Silk.ElementRenderers
{
    [Element<ImageRenderableElement>]
    public class ImageElementRenderer : IElementRenderer
    {
        private readonly IDrawingContext<GL> _drawingContext;
        
        public ImageElementRenderer(IDrawingContext<GL> drawingContext)
        {
            _drawingContext = drawingContext ?? throw new ArgumentNullException(nameof(drawingContext));
        }

        public void Initialise(IRenderableElement element)
        {
            throw new NotImplementedException();
        }

        public void Render(IRenderableElement element)
        {
            throw new NotImplementedException();
        }
    }
}