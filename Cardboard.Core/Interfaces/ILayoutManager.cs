using Cardboard.Core.Models;

namespace Cardboard.Core.Interfaces
{
    public interface ILayoutManager
    {
        IReadOnlyList<IRenderableElement> Layout(IComponent root, Size availableSize);
    }
}