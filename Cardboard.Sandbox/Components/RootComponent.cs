using Cardboard.Core.Components;
using Cardboard.Core.Interfaces;

namespace Cardboard.Sandbox.Components
{
    public class RootComponent : Component
    {
        public override IElement Render()
        {
            return new StackPanel();
        }
    }
}