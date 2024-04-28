namespace StudioLaValse.ScoreDocument.Layout
{
    public interface ILayout<TMemento>
    {
        TMemento GetMemento();
        void ApplyMemento(TMemento memento);
        void Restore();
    }
    public interface IInstrumentMeasureLayout
    {
        IEnumerable<ClefChange> ClefChanges { get; }
    }
}