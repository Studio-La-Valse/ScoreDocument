namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// Represents a primitive measure block.
    /// </summary>
    public interface IMeasureBlock : IUniqueScoreElement, IPositionElement
    {
        /// <summary>
        /// Enumerate the chords in the block.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IChord> EnumerateChords();
    }
}
