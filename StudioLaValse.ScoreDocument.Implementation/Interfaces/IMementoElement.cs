namespace StudioLaValse.ScoreDocument.Implementation.Interfaces
{
    public interface IMementoElement<TMemento>
    {
        TMemento GetMemento();
        void ApplyMemento(TMemento memento);
    }
}
