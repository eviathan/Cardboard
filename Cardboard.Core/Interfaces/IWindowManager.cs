
namespace Cardboard.Core.Interfaces
{
    public interface IWindowManager
    {
        IWindow CreateWindow(string title, int width, int height);
    }
}