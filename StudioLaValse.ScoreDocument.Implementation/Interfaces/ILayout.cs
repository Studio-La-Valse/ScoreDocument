namespace StudioLaValse.ScoreDocument.Implementation.Interfaces
{
    public interface ILayout<TMemento> : IMementoElement<TMemento>
    {
        void Restore();
    }
}