using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;
using System.Collections;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class InstrumentRibbon : ScoreElement, IInstrumentRibbonEditor, IInstrumentRibbonReader
    {
        private readonly ScoreDocumentCore score;
        private readonly Instrument instrument;
        private IInstrumentRibbonLayout layout;

        
        public Instrument Instrument =>
            instrument;
        public int IndexInScore =>
            score.contentTable.IndexOf(this);



        public InstrumentRibbon(ScoreDocumentCore score, Instrument instrument, IInstrumentRibbonLayout layout, IKeyGenerator<int> keyGenerator) : base(keyGenerator)
        {
            this.score = score;
            this.instrument = instrument;
            this.layout = layout;
        }



        public IRibbonMeasureReader ReadMeasure(int index)
        {
            return score.contentTable.GetCell(index, IndexInScore);
        }
        public IRibbonMeasureEditor EditMeasure(int index)
        {
            return score.contentTable.GetCell(index, IndexInScore);
        }



        public IEnumerable<IRibbonMeasure> EnumerateMeasures()
        {
            return score.contentTable.GetCellsRow(IndexInScore);
        }
        public IEnumerable<IRibbonMeasureReader> ReadMeasures()
        {
            return score.contentTable.GetCellsRow(IndexInScore);
        }
        public IEnumerable<IRibbonMeasureEditor> EditMeasures()
        {
            return score.contentTable.GetCellsRow(IndexInScore);
        }



        public void ApplyLayout(IInstrumentRibbonLayout layout)
        {
            this.layout = layout;
        }
        public IInstrumentRibbonLayout ReadLayout()
        {
            return this.layout;
        }


    }
}
