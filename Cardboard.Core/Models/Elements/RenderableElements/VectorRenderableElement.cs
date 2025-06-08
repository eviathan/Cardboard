using Cardboard.Core.Interfaces;

namespace Cardboard.Core.Models.Elements.RenderableElements
{
    public class VectorRenderableElement : IRenderableElement
    {
        public IElement Element { get; set; } = null!;
        public Rectangle Frame { get; set; }
    }
}