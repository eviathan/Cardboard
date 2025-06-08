using Cardboard.Core.Models;

namespace Cardboard.Core.Interfaces
{
    public interface IElement
    {
        IElement Parent { get; }
        List<IElement> Children { get; }
        string? Key { get; }
        string ElementType { get; }
        Rectangle Frame { get; init; }
        public Dictionary<string, object?> Properties { get; set; }
    }
}