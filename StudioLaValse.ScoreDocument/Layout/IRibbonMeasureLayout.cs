namespace StudioLaValse.ScoreDocument.Layout
{
    public interface IRibbonMeasureLayout : IScoreElementLayout<IRibbonMeasureLayout>
    {
        IEnumerable<ClefChange> ClefChanges { get; }
        void AddClefChange(ClefChange clefChange);
        void RemoveClefChange(ClefChange clefChange);
    }
}