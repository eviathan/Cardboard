using Cardboard.Core.Models;

namespace Cardboard.Core.Interfaces
{
    public interface IRenderer : IDisposable
    {
        void Initialise(IntPtr nativeWindowHandle);
        void Render(IElement root);
        void Resize(Size newSize);
    }
}