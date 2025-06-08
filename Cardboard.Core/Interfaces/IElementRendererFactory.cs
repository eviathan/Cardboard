namespace Cardboard.Core.Interfaces
{
    public interface IElementRendererFactory
    {
        IElementRenderer GetElementRenderer(IRenderableElement element);
    }
}