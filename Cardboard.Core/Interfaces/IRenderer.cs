using Cardboard.Core.Models;

namespace Cardboard.Core.Interfaces
{
    public interface IRenderer : IDisposable
    {
        void Initialise(IntPtr nativeWindowHandle);
        void Render(IElement root, double delta);
        void Resize(Size newSize);
    }
}