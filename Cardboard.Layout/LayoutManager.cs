using Cardboard.Core.Components;
using Cardboard.Core.Interfaces;
using Cardboard.Core.Models;
using Cardboard.Core.Models.Elements.RenderableElements;

namespace Cardboard.Layout
{
    public class LayoutManager : ILayoutManager
    {
        public IReadOnlyList<IRenderableElement> Layout(IComponent root, Size availableSize)
        {
            return [
                new BoxRenderableElement
                {
                    Element = new StackPanel
                    {

                    },
                    Frame = new Rectangle
                    {
                        Position = new Vector2D
                        {
                            X = 0,
                            Y = 0
                        },
                        Size = new Size
                        {
                            Width = 400,
                            Height = 200
                        }
                    }
                }
            ];
        }
    }
}