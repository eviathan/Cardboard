using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Cardboard.Core.Interfaces;

namespace Cardboard.Core.Models
{
    public class Element : IElement
    {
        private readonly List<IElement> _children = new();

        private IElement? _parent;

        public IElement Parent => _parent!;

        public IReadOnlyList<IElement> Children => new ReadOnlyCollection<IElement>(_children);

        public virtual string? Key { get; init; }

        public virtual string ElementType => GetType().Name;

        public void AddChild(IElement child)
        {
            if (child == null)
                throw new ArgumentNullException(nameof(child));

            if (child == this)
                throw new InvalidOperationException("An element cannot be a child of itself.");

            if (_children.Contains(child))
                return;

            if (child.Parent is Element previousParent)
            {
                previousParent.RemoveChild(child);
            }

            _children.Add(child);

            if (child is Element element)
            {
                element._parent = this;
            }
        }

        public void RemoveChild(IElement child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            if (_children.Remove(child) && child is Element elem)
            {
                elem._parent = null;
            }
        }

        public void ClearChildren()
        {
            foreach (var child in _children)
            {
                if (child is Element elem)
                {
                    elem._parent = null;
                }
            }
            _children.Clear();
        }
    }
}
