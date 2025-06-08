using Cardboard.Core.Attributes;
using Cardboard.Core.Interfaces;
using Cardboard.Core.Models.Elements.RenderableElements;
using Silk.NET.OpenGL;

namespace Cardboard.Renderer.Silk.ElementRenderers
{
    [Element<BoxRenderableElement>]
    public class BoxElementRenderer : IElementRenderer
    {
        private readonly IDrawingContext<GL> _drawingContext;

        public BoxElementRenderer(IDrawingContext<GL> drawingContext)
        {
            _drawingContext = drawingContext ?? throw new ArgumentNullException(nameof(drawingContext));
        }

        public void Render(IRenderableElement element)
        {
            throw new NotImplementedException();
        }
    }
}