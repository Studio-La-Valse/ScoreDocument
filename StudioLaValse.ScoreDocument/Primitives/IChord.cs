namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// Represents a primitive chord.
    /// </summary>
    public interface IChord : IPositionElement, IUniqueScoreElement
    {
        /// <summary>
        /// Enumerate the primitives in the chord.
        /// </summary>
        /// <returns></returns>
        IEnumerable<INote> EnumerateNotes();
    }
}
