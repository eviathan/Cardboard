using Cardboard.Core.Attributes;
using Cardboard.Core.Interfaces;
using Silk.NET.OpenGL;

namespace Cardboard.Renderer.Silk.ElementRenderers
{
    [Element<TextElementRenderer>]
    public class TextElementRenderer : IElementRenderer
    {
        private readonly IDrawingContext<GL> _drawingContext;

        public TextElementRenderer(IDrawingContext<GL> drawingContext)
        {
            _drawingContext = drawingContext ?? throw new ArgumentNullException(nameof(drawingContext));
        }

        public void Render(IRenderableElement element)
        {
            throw new NotImplementedException();
        }
    }
}