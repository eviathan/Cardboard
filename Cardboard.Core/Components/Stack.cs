using Cardboard.Core.Models;

namespace Cardboard.Core.Components
{
    public class Stack<IProperties> : Component<PropertiesWithChildren<IProperties>>
    {
        public override Element Render()
        {
            return null;
            // throw new NotImplementedException();
            // return new Element(nameof(Stack), Properties: Properties);
        }
    }
}