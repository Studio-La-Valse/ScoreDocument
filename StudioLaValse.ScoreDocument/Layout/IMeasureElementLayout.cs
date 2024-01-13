namespace StudioLaValse.ScoreDocument.Layout
{
    public interface IMeasureElementLayout : IScoreElementLayout<IMeasureElementLayout>
    {
        AccidentalDisplay ForceAccidental { get; }
        int StaffIndex { get; }
        double XOffset { get; }
    }
}