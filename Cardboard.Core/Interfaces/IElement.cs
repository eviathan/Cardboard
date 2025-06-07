using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cardboard.Core.Interfaces
{
    public interface IElement
    {
        IElement Parent { get; }
        List<IElement> Children { get; }
        string? Key { get; }
        string ElementType { get; }
        public Dictionary<string, object?> Properties { get; set; }
    }
}