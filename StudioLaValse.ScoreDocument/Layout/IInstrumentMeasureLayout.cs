namespace StudioLaValse.ScoreDocument.Layout
{
    public interface IInstrumentMeasureLayout : IScoreElementLayout<IInstrumentMeasureLayout>
    {
        IEnumerable<ClefChange> ClefChanges { get; }
        void AddClefChange(ClefChange clefChange);
        void RemoveClefChange(ClefChange clefChange);
    }
}