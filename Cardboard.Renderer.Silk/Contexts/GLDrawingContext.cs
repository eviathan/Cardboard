using Cardboard.Core.Interfaces;
using Silk.NET.OpenGL;

namespace Cardboard.Renderer.Silk.Contexts
{
    public class GLDrawingContext : IDrawingContext<GL>
    {
        public GL API { get; private set; } = null!;

        public void Initialise(GL api) => API = api;
    }
}