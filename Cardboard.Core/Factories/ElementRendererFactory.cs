using Cardboard.Core.Attributes;
using Cardboard.Core.Interfaces;
using System.Reflection;

namespace Cardboard.Core.Factories
{
    public abstract class ElementRendererFactory : IElementRendererFactory
    {
        protected Dictionary<Type, IElementRenderer> Renderers { get; set; } = [];

        public ElementRendererFactory()
        {
            RegisterRenderers();
        }

        protected abstract Assembly GetTargetAssembly();

        public IElementRenderer GetElementRenderer(IRenderableElement element)
        {
            var elementType = element.GetType();

            if (Renderers.TryGetValue(elementType, out var renderer))
            {
                return renderer;
            }

            throw new ArgumentOutOfRangeException(nameof(element));
        }
        
        private void RegisterRenderers()
        {
            var assembly = GetTargetAssembly();

            foreach (var type in assembly.GetTypes())
            {
                if (!typeof(IElementRenderer).IsAssignableFrom(type) || type.IsAbstract || type.IsInterface)
                    continue;

                var attributeData = type.GetCustomAttributesData()
                    .FirstOrDefault(attr => attr.AttributeType.IsGenericType &&
                                            attr.AttributeType.GetGenericTypeDefinition() == typeof(ElementAttribute<>));

                if (attributeData is null)
                    continue;

                var targetType = attributeData.AttributeType.GetGenericArguments().First();

                if (Activator.CreateInstance(type) is IElementRenderer instance)
                {
                    Renderers[targetType] = instance;
                }
                else
                {
                    throw new InvalidOperationException($"Could not instantiate renderer: {type.FullName}");
                }
            }
        }
    }
}