using Cardboard.Core.Attributes;
using Cardboard.Core.Interfaces;
using Cardboard.Core.Models.Elements.RenderableElements;
using Silk.NET.OpenGL;

namespace Cardboard.Renderer.Silk.ElementRenderers
{
    [Element<BoxRenderableElement>]
    public class BoxElementRenderer : IElementRenderer
    {
        private readonly IDrawingContext<GL> _drawingContext;

        private static uint _vao;
        private static uint _vbo;
        private static uint _ebo;
        private static uint _program;

        public BoxElementRenderer(IDrawingContext<GL> drawingContext)
        {
            _drawingContext = drawingContext ?? throw new ArgumentNullException(nameof(drawingContext));
        }

        public unsafe void Initialise(IRenderableElement element)
        {
            _vao = _drawingContext.API.GenVertexArray();
            _drawingContext.API.BindVertexArray(_vao);

            float[] vertices =
            {
                0.5f,  0.5f, 0.0f,
                0.5f, -0.5f, 0.0f,
                -0.5f, -0.5f, 0.0f,
                -0.5f,  0.5f, 0.0f
            };

            _vbo = _drawingContext.API.GenBuffer();
            _drawingContext.API.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo);

            fixed (float* buf = vertices)
                _drawingContext.API.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(vertices.Length * sizeof(float)), buf, BufferUsageARB.StaticDraw);

            uint[] indices =
            {
                0u, 1u, 3u,
                1u, 2u, 3u
            };

            _ebo = _drawingContext.API.GenBuffer();
            _drawingContext.API.BindBuffer(BufferTargetARB.ElementArrayBuffer, _ebo);

            fixed (uint* buf = indices)
                _drawingContext.API.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint)(indices.Length * sizeof(uint)), buf, BufferUsageARB.StaticDraw);

            const string vertexCode = @"
            #version 330 core

            layout (location = 0) in vec3 aPosition;

            void main()
            {
                gl_Position = vec4(aPosition, 1.0);
            }";

            const string fragmentCode = @"
            #version 330 core

            out vec4 out_color;

            void main()
            {
                out_color = vec4(1.0, 0.5, 0.2, 1.0);
            }";

            uint vertexShader = _drawingContext.API.CreateShader(ShaderType.VertexShader);

            _drawingContext.API.ShaderSource(vertexShader, vertexCode);
            _drawingContext.API.CompileShader(vertexShader);
            _drawingContext.API.GetShader(vertexShader, ShaderParameterName.CompileStatus, out int vStatus);

            if (vStatus != (int)GLEnum.True)
                throw new Exception("Vertex shader failed to compile: " + _drawingContext.API.GetShaderInfoLog(vertexShader));

            uint fragmentShader = _drawingContext.API.CreateShader(ShaderType.FragmentShader);

            _drawingContext.API.ShaderSource(fragmentShader, fragmentCode);
            _drawingContext.API.CompileShader(fragmentShader);
            _drawingContext.API.GetShader(fragmentShader, ShaderParameterName.CompileStatus, out int fStatus);

            if (fStatus != (int)GLEnum.True)
                throw new Exception("Fragment shader failed to compile: " + _drawingContext.API.GetShaderInfoLog(fragmentShader));

            _program = _drawingContext.API.CreateProgram();

            _drawingContext.API.AttachShader(_program, vertexShader);
            _drawingContext.API.AttachShader(_program, fragmentShader);

            _drawingContext.API.LinkProgram(_program);

            _drawingContext.API.GetProgram(_program, ProgramPropertyARB.LinkStatus, out int lStatus);

            if (lStatus != (int)GLEnum.True)
                throw new Exception("Program failed to link: " + _drawingContext.API.GetProgramInfoLog(_program));

            _drawingContext.API.DetachShader(_program, vertexShader);
            _drawingContext.API.DetachShader(_program, fragmentShader);
            _drawingContext.API.DeleteShader(vertexShader);
            _drawingContext.API.DeleteShader(fragmentShader);

            const uint positionLoc = 0;
            _drawingContext.API.EnableVertexAttribArray(positionLoc);
            _drawingContext.API.VertexAttribPointer(positionLoc, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), (void*)0);
            
            _drawingContext.API.BindVertexArray(0);
            _drawingContext.API.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
            _drawingContext.API.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);
        }

        public unsafe void Render(IRenderableElement element)
        {
            _drawingContext.API.BindVertexArray(_vao);
            _drawingContext.API.UseProgram(_program);
            _drawingContext.API.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, (void*) 0);
        }
    }
}