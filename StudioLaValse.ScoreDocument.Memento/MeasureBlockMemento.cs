namespace StudioLaValse.ScoreDocument.Memento
{
    /// <summary>
    /// Represents the data necessary to create a <see cref="IMeasureBlockEditor"/>.
    /// </summary>
    public class MeasureBlockMemento
    {
        /// <summary>
        /// The chords of the measure block.
        /// </summary>
        public required IEnumerable<ChordMemento> Chords { get; init; }
        /// <summary>
        /// The layout of the measure block.
        /// </summary>
        public required INoteGroupLayout Layout { get; init; }
        /// <summary>
        /// The duration of the measure block.
        /// </summary>
        public required Duration Duration { get; init; }
        /// <summary>
        /// Whether the measure block is a grace or not.
        /// </summary>
        public required bool Grace { get; init; }
    }
}
