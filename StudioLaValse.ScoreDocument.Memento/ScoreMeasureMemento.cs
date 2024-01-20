namespace StudioLaValse.ScoreDocument.Memento
{
    /// <summary>
    /// Represents the data necessary to create a <see cref="IScoreMeasureEditor"/>.
    /// </summary>
    public class ScoreMeasureMemento
    {
        /// <summary>
        /// The isntrument measures in the score measure.
        /// </summary>
        public required IEnumerable<InstrumentMeasureMemento> Measures { get; init; }
        /// <summary>
        /// The layout of the score measure.
        /// </summary>
        public required IScoreMeasureLayout Layout { get; init; }
        /// <summary>
        /// The time signature of the score measure.
        /// </summary>
        public required TimeSignature TimeSignature { get; init; }
        /// <summary>
        /// The staff system of the score measure.
        /// </summary>
        public required StaffSystemMemento StaffSystem { get; init; }
    }
}
