namespace StudioLaValse.ScoreDocument.Primitives
{
    public interface IPositionElement : IUniqueScoreElement
    {
        bool Grace { get; }
        Position Position { get; }
        RythmicDuration RythmicDuration { get; }
        Tuplet Tuplet { get; }
    }
}
