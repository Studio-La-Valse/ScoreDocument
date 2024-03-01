namespace StudioLaValse.ScoreDocument.Core.Primitives
{
    /// <summary>
    /// Represents a primitive chord.
    /// </summary>
    public interface IChord : IPositionElement, IUniqueScoreElement
    {

    }

    /// <inheritdoc/>
    public interface IChord<TNote> : IChord where TNote : INote
    {
        /// <summary>
        /// Enumerate the primitives in the chord.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TNote> ReadNotes();
    }
}
