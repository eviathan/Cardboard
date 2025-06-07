namespace Cardboard.Core.Interfaces
{
    public interface ITreeManager
    {
        IElement RootElement { get; }
        void SetRoot(IElement root);
        void Reconcile(IElement newRoot);
        void Traverse(Action<IElement> action);
    }
}