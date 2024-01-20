namespace StudioLaValse.ScoreDocument.Memento
{
    /// <summary>
    /// Represents the data necessary to create an <see cref="IInstrumentMeasureEditor"/>.
    /// </summary>
    public class InstrumentMeasureMemento
    {
        /// <summary>
        /// The layout of the instrument measure.
        /// </summary>
        public required IInstrumentMeasureLayout Layout { get; init; }
        /// <summary>
        /// The index of the instrument measure.
        /// </summary>
        public required int MeasureIndex { get; init; }
        /// <summary>
        /// The ribbon index of the measure.
        /// </summary>
        public required int RibbonIndex { get; init; }
        /// <summary>
        /// The voice groups in the measure.
        /// </summary>
        public required IEnumerable<RibbonMeasureVoiceMemento> VoiceGroups { get; init; }
    }
}
