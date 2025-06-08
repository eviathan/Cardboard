using Cardboard.Core.Interfaces;
using Cardboard.Core.Models;

namespace Cardboard.Core.Components
{
    public class StackPanel : Component
    {
        public override IElement Render()
        {
            return new StackPanel();
        }
    }
}