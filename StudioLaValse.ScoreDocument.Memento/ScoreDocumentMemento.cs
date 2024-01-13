namespace StudioLaValse.ScoreDocument.Memento
{
    public class ScoreDocumentMemento
    {
        public required IScoreDocumentLayout Layout { get; init; }
        public required IEnumerable<InstrumentRibbonMemento> InstrumentRibbons { get; init; }
        public required IEnumerable<ScoreMeasureMemento> ScoreMeasures { get; init; }
    }
}
