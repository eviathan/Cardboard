
using Cardboard.Core.Models;

namespace Cardboard.Core.Interfaces
{
    public interface IRenderableElement
    {
        IElement Element { get; set; }
        Rectangle Frame { get; set; }
    }
}