namespace Cardboard.Core.Interfaces
{
    public interface IDrawingContext<TAPI>
    {
        TAPI API { get; }
        void Initialise(TAPI api);
    }
}