using Cardboard.Core.Interfaces;

namespace Cardboard.Renderer.Silk.Factories
{
    public class SilkElementRendererFactory : IElementRendererFactory
    {
        private Dictionary<Type, IElementRenderer> _renderers { get; set; } = [];

        public SilkElementRendererFactory()
        {
            //  TODO: Populate dictionary with all element renderers in this    
        }
        public IElementRenderer GetElementRenderer(IRenderableElement element)
        {
            var elementType = element.GetType();

            if (_renderers.TryGetValue(elementType, out var renderer))
            {
                return renderer;
            }

            throw new ArgumentOutOfRangeException(nameof(element));
        }
    }
}