using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;
using System.Collections;

namespace StudioLaValse.ScoreDocument.Layout.Private.Readers
{
    internal class StaffGroupReaderWithLayoutDictionary : IStaffGroupReader
    {
        private readonly IStaffGroupReader source;
        private readonly IScoreLayoutDictionary layoutDictionary;

        public StaffGroupReaderWithLayoutDictionary(IStaffGroupReader source, IScoreLayoutDictionary layoutDictionary)
        {
            this.source = source;
            this.layoutDictionary = layoutDictionary;
        }

        public Instrument Instrument => source.Instrument;

        public int Id => source.Id;

        public IEnumerable<IInstrumentMeasure> EnumerateMeasures()
        {
            return source.EnumerateMeasures();
        }

        public IEnumerable<IStaff> EnumerateStaves()
        {
            return source.EnumerateStaves();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public IInstrumentRibbonReader ReadContext()
        {
            return source.ReadContext();
        }

        public IStaffGroupLayout ReadLayout()
        {
            return layoutDictionary.GetOrCreate(source);
        }

        public IEnumerable<IInstrumentMeasureReader> ReadMeasures()
        {
            return source.ReadMeasures();
        }

        public IEnumerable<IStaffReader> ReadStaves()
        {
            return source.ReadStaves().Select(e => e.UseLayout(layoutDictionary));
        }
    }
}
