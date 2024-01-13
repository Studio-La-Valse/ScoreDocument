namespace StudioLaValse.ScoreDocument.Primitives
{
    public interface IMeasureBlock : IUniqueScoreElement
    {
        bool Grace { get; }
        Duration Duration { get; }
        IEnumerable<IChord> EnumerateChords();
    }
}
