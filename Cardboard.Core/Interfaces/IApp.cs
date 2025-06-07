namespace Cardboard.Core.Interfaces
{
    public interface IApp
    {
        void Run();
        void Exit();
        void SetRootComponent<TComponent>(IWindow window)
            where TComponent : IElement, new();
    }
}