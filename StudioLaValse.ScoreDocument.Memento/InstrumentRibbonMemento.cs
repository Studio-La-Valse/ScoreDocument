namespace StudioLaValse.ScoreDocument.Memento
{
    /// <summary>
    /// Represents the data necessary to create an <see cref="IInstrumentRibbonEditor"/>.
    /// </summary>
    public class InstrumentRibbonMemento
    {
        /// <summary>
        /// The layout of the instrument ribbon.
        /// </summary>
        public required IInstrumentRibbonLayout Layout { get; init; }
        /// <summary>
        /// The measures in the instrument ribbon.
        /// </summary>
        public required IEnumerable<InstrumentMeasureMemento> Measures { get; init; }
        /// <summary>
        /// The instrument of the instrument ribbon.
        /// </summary>
        public required Instrument Instrument { get; init; }
    }
}
