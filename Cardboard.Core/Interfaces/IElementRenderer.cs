namespace Cardboard.Core.Interfaces
{
    public interface IElementRenderer
    {
        void Initialise(IRenderableElement element);
        void Render(IRenderableElement element);
    }
}