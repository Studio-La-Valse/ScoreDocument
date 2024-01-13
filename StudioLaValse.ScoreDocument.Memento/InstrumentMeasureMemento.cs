namespace StudioLaValse.ScoreDocument.Memento
{
    public class InstrumentMeasureMemento
    {
        public required IInstrumentMeasureLayout Layout { get; init; }
        public required int MeasureIndex { get; init; }
        public required int RibbonIndex { get; init; }
        public required IEnumerable<RibbonMeasureVoiceMemento> VoiceGroups { get; init; }
    }
}
