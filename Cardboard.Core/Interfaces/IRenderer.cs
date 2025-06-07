using Cardboard.Core.Models;

namespace Cardboard.Core.Interfaces
{
    public interface IRenderer : IDisposable
    {
        void Initialize(IntPtr nativeWindowHandle);
        void Render(IElement root);
        void Resize(Size newSize);
    }
}