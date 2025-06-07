using Cardboard.Core.Models;

namespace Cardboard.Core.Components
{
    public abstract class Component : Component<object> { }
    
    public abstract class Component<TProperties> : Element
        where TProperties : class, new()
    {
        public TProperties? Properties { get; private set; }

        public abstract Element Render();

        public void SetProperties(TProperties properties)
        {
            Properties = properties;
        }
    }
}