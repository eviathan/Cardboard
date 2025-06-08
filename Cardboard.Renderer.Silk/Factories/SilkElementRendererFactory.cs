using System.Reflection;
using Cardboard.Core.Factories;

namespace Cardboard.Renderer.Silk.Factories
{
    public class SilkElementRendererFactory : ElementRendererFactory
    {
        public SilkElementRendererFactory(IServiceProvider serviceProvider) 
            : base(serviceProvider) { }

        protected override Assembly GetTargetAssembly()
        {
            return typeof(SilkElementRendererFactory).Assembly;
        }
    }
}