namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// Represents a primitive measure block.
    /// </summary>
    public interface IMeasureBlock : IUniqueScoreElement
    {
        /// <summary>
        /// Specifies whether the block is a grace block.
        /// If it is a grace block, the duration will be zero.
        /// </summary>
        bool Grace { get; }
        /// <summary>
        /// The duration of the block.
        /// If the block is a grace, the duration will be 0.
        /// </summary>
        Duration Duration { get; }
        /// <summary>
        /// Enumerate the chords in the block.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IChord> EnumerateChords();
    }
}
