using Cardboard.Core.Interfaces;

namespace Cardboard.Core.Models.Elements.RenderableElements
{
    public class BoxRenderableElement : IRenderableElement
    {
        public IElement Element { get; set; } = null!;
        public Rectangle Frame { get; set; }
    }
}