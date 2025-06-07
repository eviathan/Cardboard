using System.ComponentModel;
using Silk.NET.Windowing;
using Silk.NET.Maths;
using Cardboard.Core.Interfaces;
using Silk.NET.OpenGL;

using ICardboardWindow = Cardboard.Core.Interfaces.IWindow;
using ISilkWindow = Silk.NET.Windowing.IWindow;
using IComponent = Cardboard.Core.Interfaces.IComponent;

namespace Cardboard.Renderer.Silk
{
    public class SilkWindow : ICardboardWindow
    {
        private readonly string _title;
        private readonly int _width;
        private readonly int _height;

        private readonly ISilkWindow? _window;
        private readonly IRenderer? _renderer;
        private IComponent? _rootComponent;

        private GL? _gl;

        public SilkWindow(string title, int width, int height)
        {
            _title = title ?? throw new ArgumentNullException(nameof(title));
            _width = width;
            _height = height;

            var opts = WindowOptions.Default with
            {
                Title = title,
                Size = new Vector2D<int>(width, height)
            };

            _window = Window.Create(opts);
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void SetRootComponent(IComponent component)
        {
            _rootComponent = component ?? throw new ArgumentNullException(nameof(component));
        }

        public void Show()
        {
            if (_window != null)
            {
                _window.Load += OnLoad;
                _window.Render += OnRender;
                _window.Run();       
            }
        }

        private void OnLoad()
        {
            if (_window is null) return;
            _gl = GL.GetApi(_window);

            _gl.ClearColor(0f, 0f, 0f, 1f);
    
            // _renderer.Initialize(_window); // You can pass in Silk.NET GLContext, etc.
            // _renderer.SetRootComponent(_root);
        }

        private void OnRender(double delta)
        {
            if (_gl == null) return;

            _gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            // _renderer.RenderFrame(delta);
        }
    }
}