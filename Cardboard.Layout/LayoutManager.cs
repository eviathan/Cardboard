using Cardboard.Core.Interfaces;
using Cardboard.Core.Models;

namespace Cardboard.Layout
{
    public class LayoutManager : ILayoutManager
    {
        public IReadOnlyList<IRenderableElement> Layout(IComponent root, Size availableSize)
        {
            return [];
        }
    }
}