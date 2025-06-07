using Cardboard.Core.Models;

namespace Cardboard.Core.Components
{
    public class Stack<IProperties> : Component
        where IProperties : class, new()
    {
        public override Element Render()
        {
            return new Element();
            // throw new NotImplementedException();
            // return new Element(nameof(Stack), Properties: Properties);
        }
    }
}