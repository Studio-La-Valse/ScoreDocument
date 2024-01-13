using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Layout.Private.Readers
{
    internal class ScoreDocumentReaderWithLayoutDictionary : IScoreDocumentReader
    {
        private readonly IScoreDocumentReader source;
        private readonly IScoreLayoutDictionary layoutDictionary;

        public ScoreDocumentReaderWithLayoutDictionary(IScoreDocumentReader source, IScoreLayoutDictionary layoutDictionary)
        {
            this.source = source;
            this.layoutDictionary = layoutDictionary;
        }

        public int Id => source.Id;

        public int NumberOfMeasures => source.NumberOfMeasures;

        public int NumberOfInstruments => source.NumberOfInstruments;

        public IEnumerable<IInstrumentRibbon> EnumerateInstrumentRibbons()
        {
            return source.EnumerateInstrumentRibbons();
        }

        public IEnumerable<IScoreMeasure> EnumerateScoreMeasures()
        {
            return source.EnumerateScoreMeasures();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public IInstrumentRibbonReader ReadInstrumentRibbon(int indexInScore)
        {
            return source.ReadInstrumentRibbon(indexInScore);
        }

        public IEnumerable<IInstrumentRibbonReader> ReadInstrumentRibbons()
        {
            return source.ReadInstrumentRibbons().Select(e => e.UseLayout(layoutDictionary));
        }

        public IScoreDocumentLayout ReadLayout()
        {
            return layoutDictionary.GetOrCreate(source);
        }

        public IScoreMeasureReader ReadMeasure(int indexInScore)
        {
            return source.ReadMeasure(indexInScore).UseLayout(layoutDictionary);
        }

        public IEnumerable<IScoreMeasureReader> ReadScoreMeasures()
        {
            return source.ReadScoreMeasures().Select(e => e.UseLayout(layoutDictionary));
        }

        public IEnumerable<IStaffSystemReader> ReadStaffSystems()
        {
            if (!ReadScoreMeasures().Any())
            {
                yield break;
            }

            var firstMeasure = ReadScoreMeasures().First();
            if (!firstMeasure.ReadLayout().IsNewSystem)
            {
                throw new Exception("The first measure of the score must have a new system in it's layout supplied before the score can enumerate the staff systems.");
            }

            foreach (var measure in ReadScoreMeasures())
            {
                if (measure.ReadLayout().IsNewSystem)
                {
                    yield return measure.ReadStaffSystemOrigin();
                }
            }
        }
    }
}
