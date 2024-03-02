namespace StudioLaValse.ScoreDocument.Core.Primitives
{
    /// <summary>
    /// Represents a primitive note.
    /// </summary>
    public interface INote : IPositionElement
    {
        /// <summary>
        /// The pitch of the note.
        /// </summary>
        Pitch Pitch { get; }
    }
}
