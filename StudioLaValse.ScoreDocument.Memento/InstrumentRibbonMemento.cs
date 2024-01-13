namespace StudioLaValse.ScoreDocument.Memento
{
    public class InstrumentRibbonMemento
    {
        public required IInstrumentRibbonLayout Layout { get; init; }
        public required IEnumerable<InstrumentMeasureMemento> Measures { get; init; }
        public required Instrument Instrument { get; init; }
    }
}
