using Cardboard.Core.Interfaces;
using Cardboard.Core.Models;

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

            if (RootElement == null)
            {
                RootElement = newRoot;
                return;
            }

            RootElement = ReconcileNode(RootElement, newRoot);
        }

        private IElement ReconcileNode(IElement oldNode, IElement newNode)
        {
            // Replace node if keys or types differ
            if (oldNode.Key != newNode.Key || oldNode.ElementType != newNode.ElementType)
            {
                // Optionally dispose oldNode here
                return newNode;
            }

            // Update properties
            UpdateProperties(oldNode, newNode);

            // Reconcile children
            ReconcileChildren(oldNode, newNode);

            return oldNode;
        }

        private void UpdateProperties(IElement oldNode, IElement newNode)
        {
            if (oldNode is Element oldElement && newNode is Element newElement)
            {
                // Simple approach: replace properties dictionary
                oldElement.Properties = new Dictionary<string, object?>(newElement.Properties);
            }
            // Add your own property update logic for different IElement implementations
        }

        private void ReconcileChildren(IElement oldNode, IElement newNode)
        {
            if (oldNode is not Element oldElement || newNode is not Element newElement)
                return;

            var oldChildren = oldElement.Children;
            var newChildren = newElement.Children;

            var reconciledChildren = new List<IElement>();

            // Index for non-keyed children
            int oldIndex = 0;

            // Map old keyed children by key
            var oldKeyed = oldChildren.Where(c => c.Key != null).ToDictionary(c => c.Key!);

            foreach (var newChild in newChildren)
            {
                if (newChild.Key != null)
                {
                    if (oldKeyed.TryGetValue(newChild.Key, out var matchingOldChild))
                    {
                        reconciledChildren.Add(ReconcileNode(matchingOldChild, newChild));
                        oldKeyed.Remove(newChild.Key);
                    }
                    else
                    {
                        // New keyed child
                        reconciledChildren.Add(newChild);
                    }
                }
                else
                {
                    // No key: use next old child without key
                    while (oldIndex < oldChildren.Count && oldChildren[oldIndex].Key != null)
                        oldIndex++;

                    if (oldIndex < oldChildren.Count)
                    {
                        reconciledChildren.Add(ReconcileNode(oldChildren[oldIndex], newChild));
                        oldIndex++;
                    }
                    else
                    {
                        reconciledChildren.Add(newChild);
                    }
                }
            }

            // Optionally dispose remaining old keyed children in oldKeyed.Values

            // Replace old children list with reconciled
            oldElement.Children.Clear();
            oldElement.Children.AddRange(reconciledChildren);
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

            if (element is Element elem && elem.Children != null)
            {
                foreach (var child in elem.Children)
                {
                    TraverseRecursive(child, action);
                }
            }
        }
    }
}