using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Layout.Private.Readers
{
    internal class StaffSystemReaderWithLayoutDictionary : IStaffSystemReader
    {
        private readonly IStaffSystemReader source;
        private readonly IScoreLayoutDictionary layoutDictionary;

        public StaffSystemReaderWithLayoutDictionary(IStaffSystemReader source, IScoreLayoutDictionary layoutDictionary)
        {
            this.source = source;
            this.layoutDictionary = layoutDictionary;
        }

        public int Id => source.Id;

        public IEnumerable<IScoreMeasure> EnumerateMeasures()
        {
            return source.EnumerateMeasures();
        }

        public IEnumerable<IStaffGroup> EnumerateStaffGroups()
        {
            return source.EnumerateStaffGroups();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public IStaffSystemLayout ReadLayout()
        {
            return layoutDictionary.GetOrCreate(source);
        }

        public IEnumerable<IScoreMeasureReader> ReadMeasures()
        {
            return source.ReadMeasures().Select(e => e.UseLayout(layoutDictionary));
        }

        public IEnumerable<IStaffGroupReader> ReadStaffGroups()
        {
            return source.ReadStaffGroups().Select(e => e.UseLayout(layoutDictionary));
        }

        public IStaffGroupReader ReadStaffGroup(IInstrumentRibbonReader instrumentRibbon)
        {
            return source.ReadStaffGroup(instrumentRibbon).UseLayout(layoutDictionary);
        }
    }
}
