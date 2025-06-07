using Cardboard.Core.Interfaces;

namespace Cardboard.Core.Managers
{
    public class TreeManager : ITreeManager
    {
        public IElement RootElement { get; private set; } = null!;

        public void SetRoot(IElement rootElement)
        {
            RootElement = rootElement ?? throw new ArgumentNullException(nameof(rootElement));
        }

        public void Reconcile(IElement newRoot)
        {
            if (newRoot == null) throw new ArgumentNullException(nameof(newRoot));

            // For now, simple replace strategy:
            // Replace the current root with newRoot entirely.
            RootElement = newRoot;

            // TODO: Implement a proper diff and patch algorithm to update minimal nodes.
        }

        public void Traverse(Action<IElement> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            if (RootElement == null)
                return;

            TraverseRecursive(RootElement, action);
        }

        private void TraverseRecursive(IElement element, Action<IElement> action)
        {
            action(element);

            if (element.Children == null) return;

            foreach (var child in element.Children)
            {
                TraverseRecursive(child, action);
            }
        }
    }
}