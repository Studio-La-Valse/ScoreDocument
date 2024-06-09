namespace StudioLaValse.ScoreDocument.Implementation
{
    public class InstrumentMeasureFactory(IKeyGenerator<int> keyGenerator)
    {
        private readonly IKeyGenerator<int> keyGenerator = keyGenerator;

        public InstrumentMeasure Create(ScoreMeasure column, InstrumentRibbon row, ScoreDocumentStyleTemplate styleTemplate)
        {
            var layout = new AuthorInstrumentMeasureLayout(row.Instrument, column);
            var secondaryLayout = new UserInstrumentMeasureLayout(layout, Guid.NewGuid());
            return new InstrumentMeasure(column, row, styleTemplate, layout, secondaryLayout, keyGenerator, Guid.NewGuid());
        }
    }
}
