namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// Represents a primitive chord.
    /// </summary>
    public interface IChord : IScoreElement, IPositionElement
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
