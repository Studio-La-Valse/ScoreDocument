using StudioLaValse.ScoreDocument.Implementation.Private.Layout;
using StudioLaValse.ScoreDocument.Models.V1.StyleTemplates;

namespace StudioLaValse.ScoreDocument.Implementation.Private
{
    internal class InstrumentMeasureFactory(IKeyGenerator<int> keyGenerator)
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
