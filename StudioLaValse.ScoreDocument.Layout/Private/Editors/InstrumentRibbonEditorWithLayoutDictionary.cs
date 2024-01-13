using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;
using System.Collections;

namespace StudioLaValse.ScoreDocument.Layout.Private.Editors
{
    internal class InstrumentRibbonEditorWithLayoutDictionary : IInstrumentRibbonEditor
    {
        private readonly IInstrumentRibbonEditor instrumentRibbon;
        private readonly IScoreLayoutDictionary layoutDictionary;

        public InstrumentRibbonEditorWithLayoutDictionary(IInstrumentRibbonEditor instrumentRibbon, IScoreLayoutDictionary layoutDictionary)
        {
            this.instrumentRibbon = instrumentRibbon;
            this.layoutDictionary = layoutDictionary;
        }


        public Instrument Instrument => instrumentRibbon.Instrument;

        public int Id => instrumentRibbon.Id;

        public int IndexInScore => instrumentRibbon.IndexInScore;

        public void ApplyLayout(IInstrumentRibbonLayout layout)
        {
            layoutDictionary.Assign(instrumentRibbon, layout);
        }

        public IInstrumentMeasureEditor EditMeasure(int indexInScore)
        {
            return instrumentRibbon.EditMeasure(indexInScore).UseLayout(layoutDictionary);
        }

        public IEnumerable<IInstrumentMeasureEditor> EditMeasures()
        {
            return instrumentRibbon.EditMeasures().Select(e => e.UseLayout(layoutDictionary));
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return instrumentRibbon.Equals(other);
        }

        public IEnumerable<IInstrumentMeasure> EnumerateMeasures()
        {
            return instrumentRibbon.EnumerateMeasures();
        }

        public IInstrumentRibbonLayout ReadLayout()
        {
            return layoutDictionary.GetOrCreate(instrumentRibbon);
        }
    }
}
