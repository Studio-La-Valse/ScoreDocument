using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Layout.Private.Readers
{
    internal class NoteReaderWithLayoutDictionary : INoteReader
    {
        private readonly INoteReader source;
        private readonly IScoreLayoutDictionary layoutDictionary;

        public NoteReaderWithLayoutDictionary(INoteReader source, IScoreLayoutDictionary layoutDictionary)
        {
            this.source = source;
            this.layoutDictionary = layoutDictionary;
        }

        public int Id => source.Id;

        public Pitch Pitch => source.Pitch;

        public int Voice => source.Voice;

        public bool Grace => source.Grace;

        public Position Position => source.Position;

        public RythmicDuration RythmicDuration => source.RythmicDuration;

        public Tuplet Tuplet => source.Tuplet;

        public bool Equals(IUniqueScoreElement? other)
        {
            return source.Equals(other);
        }

        public IChordReader ReadContext()
        {
            return source.ReadContext().UseLayout(layoutDictionary);
        }

        public IMeasureElementLayout ReadLayout()
        {
            return layoutDictionary.GetOrCreate(source);
        }
    }
}
