using Cardboard.Core.Models;

namespace Cardboard.Core.Interfaces
{
    public interface IRenderer : IDisposable
    {
        void Initialise(IntPtr nativeWindowHandle);
        void Render(IEnumerable<IRenderableElement> elements, double delta);
        void Resize(IEnumerable<IRenderableElement> elements, Size newSize);
    }
}