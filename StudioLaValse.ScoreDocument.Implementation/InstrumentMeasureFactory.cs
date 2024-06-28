using StudioLaValse.ScoreDocument.Templates;

namespace StudioLaValse.ScoreDocument.Implementation
{
    public class InstrumentMeasureFactory(IKeyGenerator<int> keyGenerator)
    {
        private readonly IKeyGenerator<int> keyGenerator = keyGenerator;

        public InstrumentMeasure Create(ScoreMeasure column, InstrumentRibbon row, ScoreDocumentStyleTemplate styleTemplate)
        {
            var layout = new AuthorInstrumentMeasureLayout(column);
            var secondaryLayout = new UserInstrumentMeasureLayout(layout, Guid.NewGuid(), column);
            return new InstrumentMeasure(column, row, styleTemplate, layout, secondaryLayout, keyGenerator, Guid.NewGuid());
        }
    }
}
