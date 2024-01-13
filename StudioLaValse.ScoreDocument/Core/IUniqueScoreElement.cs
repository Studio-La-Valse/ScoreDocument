namespace StudioLaValse.ScoreDocument.Core
{
    public interface IUniqueScoreElement : IEquatable<IUniqueScoreElement>
    {
        int Id { get; }
    }
}
