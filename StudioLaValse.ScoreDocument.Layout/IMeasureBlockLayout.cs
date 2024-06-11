namespace StudioLaValse.ScoreDocument.Layout
{
    public interface IMeasureBlockLayout
    {
        StemDirection StemDirection { get; }
        double StemLength { get; }
        double BeamAngle { get; }
        double BeamThickness { get; }
        double BeamSpacing { get; }
    }
}