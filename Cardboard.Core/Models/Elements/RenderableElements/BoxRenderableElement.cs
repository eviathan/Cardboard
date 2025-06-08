using Cardboard.Core.Interfaces;

namespace Cardboard.Core.Models.Elements.RenderableElements
{
    public class BoxRenderableElement : IRenderableElement
    {
        public IElement Element { get; set; }
        public System.Drawing.Rectangle Frame { get; set; }
    }
}