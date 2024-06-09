namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// Represents a primitive note.
    /// </summary>
    public interface INote : IScoreElement, IUniqueScoreElement
    {
        /// <summary>
        /// The pitch of the note.
        /// </summary>
        Pitch Pitch { get; }
    }
}
