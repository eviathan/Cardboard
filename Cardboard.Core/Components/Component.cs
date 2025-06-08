using Cardboard.Core.Interfaces;
using Cardboard.Core.Models.Elements;

namespace Cardboard.Core.Components
{
    public abstract class Component : Element, IComponent
    {
        public abstract IElement Render();

        protected virtual void OnInit() { }
        protected virtual void OnUpdate() { }
        protected virtual void OnDispose() { }
    }
}