using Cardboard.Core.Attributes;
using Cardboard.Core.Interfaces;
using Cardboard.Core.Models.Elements.RenderableElements;
using Silk.NET.OpenGL;
using System.Numerics;

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

            // Vertices for a unit square centered at (0,0)
            float[] vertices =
            {
                0.5f,  0.5f, 0.0f, // Top-right
                0.5f, -0.5f, 0.0f, // Bottom-right
                -0.5f, -0.5f, 0.0f, // Bottom-left
                -0.5f,  0.5f, 0.0f  // Top-left
            };

            _vbo = _drawingContext.API.GenBuffer();
            _drawingContext.API.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo);

            fixed (float* buf = vertices)
                _drawingContext.API.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(vertices.Length * sizeof(float)), buf, BufferUsageARB.StaticDraw);

            uint[] indices =
            {
                0u, 1u, 3u, // First triangle
                1u, 2u, 3u  // Second triangle
            };

            _ebo = _drawingContext.API.GenBuffer();
            _drawingContext.API.BindBuffer(BufferTargetARB.ElementArrayBuffer, _ebo);

            fixed (uint* buf = indices)
                _drawingContext.API.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint)(indices.Length * sizeof(uint)), buf, BufferUsageARB.StaticDraw);

            const string vertexCode = @"
            #version 330 core

            layout (location = 0) in vec3 aPosition;

            uniform mat4 uTransform;

            void main()
            {
                gl_Position = uTransform * vec4(aPosition, 1.0);
            }";

            const string fragmentCode = @"
            #version 330 core

            out vec4 out_color;

            void main()
            {
                out_color = vec4(1.0, 0.5, 0.2, 1.0); // Orange color
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
            var frame = element.Frame;
            var gl = _drawingContext.API;

            var viewport = new int[4];
            gl.GetInteger(GetPName.Viewport, viewport);
            float screenWidth = viewport[2];
            float screenHeight = viewport[3];

            float x = (float)frame.Position.X;
            float y = (float)frame.Position.Y;
            float width = (float)frame.Size.Width;
            float height = (float)frame.Size.Height;

            // Calculate the scaling factors for NDC.
            // Since NDC space is from -1 to 1 (a span of 2 units),
            // we multiply the normalized pixel dimensions by 2.0.
            float scaleX_NDC = (width / screenWidth) * 2.0f;
            float scaleY_NDC = (height / screenHeight) * 2.0f;

            // Calculate the center of the box in Normalized Device Coordinates (NDC).
            // (x,y) is the top-left pixel coordinate.
            // NDC X: Convert pixel x to [-1, 1], then add half of the NDC width to get the center.
            float posX_NDC_center = ((x / screenWidth) * 2f) - 1f + (scaleX_NDC / 2f);
            // NDC Y: Convert pixel y to [1, -1] (top is 1, bottom is -1), then subtract half of the NDC height to get the center.
            float posY_NDC_center = 1f - ((y / screenHeight) * 2f) - (scaleY_NDC / 2f);

            // Create the transformation matrix: Scale then Translate.
            // The scaling makes the unit box (-0.5 to 0.5) the correct NDC size.
            // The translation moves the center of this scaled box to the calculated NDC center.
            Matrix4x4 transform =
                Matrix4x4.CreateScale(scaleX_NDC, scaleY_NDC, 1.0f) *
                Matrix4x4.CreateTranslation(posX_NDC_center, posY_NDC_center, 0.0f);

            int location = gl.GetUniformLocation(_program, "uTransform");

            if (location == -1)
                throw new Exception("Could not find uTransform uniform");

            gl.UseProgram(_program);
            gl.UniformMatrix4(location, 1, false, (float*)&transform);

            gl.BindVertexArray(_vao);
            gl.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, (void*)0);
        }
    }
}