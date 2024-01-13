namespace StudioLaValse.ScoreDocument.Primitives
{
    public interface INote : IPositionElement, IUniqueScoreElement
    {
        Pitch Pitch { get; }
        int Voice { get; }
    }
}
