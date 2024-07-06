namespace StudioLaValse.ScoreDocument.Implementation.Private.Interfaces
{
    internal interface IMementoElement<TMemento>
    {
        TMemento GetMemento();
        void ApplyMemento(TMemento memento);
    }
}
