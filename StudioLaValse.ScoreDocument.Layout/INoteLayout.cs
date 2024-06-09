namespace StudioLaValse.ScoreDocument.Layout
{
    public interface INoteLayout
    {
        AccidentalDisplay ForceAccidental { get; }
        double Scale { get; }
        int StaffIndex { get; }
        double XOffset { get; }
    }
}