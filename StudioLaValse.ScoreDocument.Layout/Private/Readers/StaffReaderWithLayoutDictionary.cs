using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Layout.Private.Readers
{
    internal class StaffReaderWithLayoutDictionary : IStaffReader
    {
        private readonly IStaffReader source;
        private readonly IScoreLayoutDictionary layoutDictionary;

        public StaffReaderWithLayoutDictionary(IStaffReader source, IScoreLayoutDictionary layoutDictionary)
        {
            this.source = source;
            this.layoutDictionary = layoutDictionary;
        }

        public int IndexInStaffGroup => source.IndexInStaffGroup;

        public int Id => source.Id;

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public IStaffLayout ReadLayout()
        {
            return layoutDictionary.GetOrCreate(source);
        }
    }
}
