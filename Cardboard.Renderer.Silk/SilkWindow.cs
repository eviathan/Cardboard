using Silk.NET.Windowing;
using Silk.NET.Maths;
using Cardboard.Core.Interfaces;
using Silk.NET.OpenGL;
using Cardboard.Core.Models;
using Cardboard.Renderer.Silk.WindowCustomisers;

using ICardboardWindow = Cardboard.Core.Interfaces.IWindow;
using ISilkWindow = Silk.NET.Windowing.IWindow;
using IComponent = Cardboard.Core.Interfaces.IComponent;
using System.Drawing;
using Size = Cardboard.Core.Models.Size;

namespace Cardboard.Renderer.Silk
{
    public class SilkWindow : ICardboardWindow
    {
        public Guid Id { get; } = Guid.NewGuid();

        private readonly ISilkWindow? _window;
        private readonly IRenderer _renderer;
        private readonly ILayoutManager _layoutManager;
        private readonly IDrawingContext<GL> _drawingContext;

        private readonly string _title;
        private readonly int _width;
        private readonly int _height;

        private IComponent? _rootComponent;

        public IntPtr NativeHandle
        {
            get
            {
                if (OperatingSystem.IsMacOS())
                    return _window!.Native?.Cocoa ?? IntPtr.Zero;
                else
                    throw new PlatformNotSupportedException("NativeHandle only implemented for macOS");
            }
        }

        public SilkWindow(string title, int width, int height, IRenderer renderer, ILayoutManager layoutManager, IDrawingContext<GL> drawingContext)
        {
            _title = title ?? throw new ArgumentNullException(nameof(title));
            _layoutManager = layoutManager ?? throw new ArgumentNullException(nameof(layoutManager));
            _renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            _drawingContext = drawingContext ?? throw new ArgumentNullException(nameof(drawingContext));

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
            _window?.Close();
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
                _window.Closing += OnClosing;
                _window.Run();       
            }
        }

        private void OnClosing()
        {
            Console.WriteLine("Window Closed!");
        }

        private void OnLoad()
        {
            if (_window is null || _rootComponent is null)
                return;

            #region Code for later
            // NOTE: THIS IS HOW WE INTEROP WITH NATIVE (SPECIFICALLY MAC) WINDOWS CURRENTLY USED TO MAKE WINDOW BORDERLESS
            if (OperatingSystem.IsMacOS())
            {
                var nativeHandle = _window.Native!.Cocoa;
                MacOsWindowCustomiser.EnableNativeDragAndTransparency(nativeHandle!.Value);
            }
            #endregion

            _drawingContext.Initialise(GL.GetApi(_window));

            // _gl.ClearColor(.12f, 0.12f, 0.12f, 1f);
            // _drawingContext.API.ClearColor(0.73f, 0.75f, 0.78f, 1f);
            _drawingContext.API.ClearColor(Color.CornflowerBlue);
    
            var availableSize = new Size(_window.Size.X, _window.Size.Y);
            var renderableElements = _layoutManager.Layout(_rootComponent, availableSize);

            _renderer.Initialise(renderableElements, NativeHandle);
        }

        private void OnRender(double delta)
        {
            if (_drawingContext.API == null || _window == null || _rootComponent == null)
                return;

            _drawingContext.API.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            var availableSize = new Size(_window.Size.X, _window.Size.Y);
            var renderableElements = _layoutManager.Layout(_rootComponent, availableSize);

            _renderer.Render(renderableElements, delta);
        }

        private void OnResize(Vector2D<int> newSize)
        {
            if (_rootComponent == null)
                return;

            var size = new Size(newSize.X, newSize.Y);
            var renderableElements = _layoutManager.Layout(_rootComponent, size);
            
            _renderer.Resize(renderableElements, size);
        }
    }
}