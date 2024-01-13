using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;
using System.Collections;

namespace StudioLaValse.ScoreDocument.Layout.Private.Readers
{
    internal class InstrumentRibbonReaderWithLayoutDictionary : IInstrumentRibbonReader
    {
        private readonly IInstrumentRibbonReader source;
        private readonly IScoreLayoutDictionary layoutDictionary;

        public InstrumentRibbonReaderWithLayoutDictionary(IInstrumentRibbonReader source, IScoreLayoutDictionary layoutDictionary)
        {
            this.source = source;
            this.layoutDictionary = layoutDictionary;
        }

        public Instrument Instrument => source.Instrument;

        public int Id => source.Id;

        public int IndexInScore => source.IndexInScore;

        public IEnumerable<IRibbonMeasure> EnumerateMeasures()
        {
            return source.EnumerateMeasures();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public IInstrumentRibbonLayout ReadLayout()
        {
            return layoutDictionary.GetOrCreate(source);
        }

        public IRibbonMeasureReader ReadMeasure(int indexInScore)
        {
            return source.ReadMeasure(indexInScore).UseLayout(layoutDictionary);
        }

        public IEnumerable<IRibbonMeasureReader> ReadMeasures()
        {
            return source.ReadMeasures().Select(e => e.UseLayout(layoutDictionary));
        }
    }
}
