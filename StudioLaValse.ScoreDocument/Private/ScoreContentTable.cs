namespace StudioLaValse.ScoreDocument.Private
{
    internal sealed class ScoreContentTable : Table<InstrumentMeasure, ScoreMeasure, InstrumentRibbon>
    {
        public ScoreContentTable(IKeyGenerator<int> keyGenerator) : base(new CellFactory(keyGenerator))
        {

        }
    }
}
