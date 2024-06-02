namespace StudioLaValse.ScoreDocument.Layout
{
    public interface IMeasureBlockLayout
    {
        double StemLength { get; }
        double BeamAngle { get; }
        double BeamThickness { get; }
        double BeamSpacing { get; }
    }
}