namespace StudioLaValse.ScoreDocument.Core
{
    /// <summary>
    /// Represents a unique and persistent element in the score.
    /// </summary>
    public interface IUniqueScoreElement : IEquatable<IUniqueScoreElement>
    {
        /// <summary>
        /// The element id of the element.
        /// </summary>
        int Id { get; }
    }
}
