using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class CellFactory : ICellFactory<RibbonMeasure, ScoreMeasure, InstrumentRibbon>
    {
        private readonly IKeyGenerator<int> keyGenerator;

        public CellFactory(IKeyGenerator<int> keyGenerator)
        {
            this.keyGenerator = keyGenerator;
        }

        public RibbonMeasure Create(ScoreMeasure column, InstrumentRibbon row)
        {
            var layout = new RibbonMeasureLayout();
            return new RibbonMeasure(column, row, layout, keyGenerator);
        }
    }
}
