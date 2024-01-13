namespace StudioLaValse.ScoreDocument.Layout
{
    public interface IChordLayout : IScoreElementLayout<IChordLayout>
    {
        double XOffset { get; }
        Dictionary<int, BeamType> Beams { get; }
    }
}