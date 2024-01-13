using StudioLaValse.ScoreDocument.Editor;

namespace StudioLaValse.ScoreDocument.Private
{
    internal sealed class ScoreContentTable : Table<RibbonMeasure, ScoreMeasure, InstrumentRibbon>
    {
        public ScoreContentTable(IKeyGenerator<int> keyGenerator) : base(new CellFactory(keyGenerator))
        {

        }
    }
}
