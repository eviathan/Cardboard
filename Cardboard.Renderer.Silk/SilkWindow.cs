using System.ComponentModel;
using Silk.NET.Windowing;
using Silk.NET.Maths;
using Cardboard.Core.Interfaces;
using Silk.NET.OpenGL;
using Cardboard.Renderer.Silk.WindowCustomisers;

using ICardboardWindow = Cardboard.Core.Interfaces.IWindow;
using ISilkWindow = Silk.NET.Windowing.IWindow;
using IComponent = Cardboard.Core.Interfaces.IComponent;
using Silk.NET.Core.Native;
using Cardboard.Core.Models;

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

        public IntPtr NativeHandle
        {
            get
            {
                if (OperatingSystem.IsMacOS())
                {
                    return _window!.Native?.Cocoa ?? IntPtr.Zero;
                }
                else
                {
                    throw new PlatformNotSupportedException("NativeHandle only implemented for macOS");
                }
            }
        }

        public SilkWindow(string title, int width, int height, IRenderer renderer)
        {
            _title = title ?? throw new ArgumentNullException(nameof(title));
            _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            _width = width;
            _height = height;

            var windowOptions = WindowOptions.Default with
            {
                Title = title,
                Size = new Vector2D<int>(width, height)
            };

            _window = Window.Create(windowOptions);
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
                _window.Resize += OnResize;
                _window.Run();       
            }
        }

        private void OnLoad()
        {
            if (_window is null) return;
            
            // NOTE: THIS IS HOW WE INTEROP WITH NATIVE (SPECIFICALLY MAC) WINDOWS CURRENTLY USED TO MAKE WINDOW BORDERLESS
            // if (OperatingSystem.IsMacOS())
            // {
            //     var nativeHandle = _window.Native!.Cocoa;
            //     MacOsWindowCustomiser.EnableNativeDragAndTransparency(nativeHandle!.Value);
            // }

            _gl = GL.GetApi(_window);
            _gl.ClearColor(.12f, 0.12f, 0.12f, 1f);
    
            _renderer.Initialise(NativeHandle);
        }

        private void OnRender(double delta)
        {
            if (_gl == null) return;

            _gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            _renderer.Render(_rootComponent as IElement);
        }

        private void OnResize(Vector2D<int> newSize)
        {
            _renderer.Resize(new Size(newSize.X, newSize.Y));
        }
    }
}