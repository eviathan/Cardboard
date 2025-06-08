
using Cardboard.Core.Components;

namespace Cardboard.Engine
{
    public class ApplicationOptions
    {
        public string Title { get; set; } = string.Empty;
        public int Width { get; set; } = 1280;
        public int Height { get; set; } = 720;
        public Type ComponentType { get; set; } = null!;

        public void SetRootComponent<TComponent>()
            where TComponent : Component
        {
            ComponentType = typeof(TComponent);
        }
    }
}