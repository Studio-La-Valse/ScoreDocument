namespace StudioLaValse.ScoreDocument.Layout
{
    public interface INoteGroupLayout : IScoreElementLayout<INoteGroupLayout>
    {
        double StemLength { get; }
        double BeamAngle { get; }
    }
}
