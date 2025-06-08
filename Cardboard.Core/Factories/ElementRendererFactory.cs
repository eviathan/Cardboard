using Cardboard.Core.Attributes;
using Cardboard.Core.Interfaces;
using System.Reflection;

namespace Cardboard.Core.Factories
{
    public abstract class ElementRendererFactory : IElementRendererFactory
    {
        private readonly IServiceProvider _serviceProvider;

        protected Dictionary<Type, IElementRenderer> Renderers { get; set; } = [];

        public ElementRendererFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

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

            // TODO: Handle exceptions properly when looking for renderers
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
                var renderer = (_serviceProvider.GetService(type) as IElementRenderer)!;

                Renderers[targetType] = renderer;
            }
        }
    }
}