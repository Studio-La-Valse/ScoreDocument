using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Reader.Extensions;

namespace StudioLaValse.ScoreDocument.Reader.Private
{
    internal class MeasureBlockReaderFromGraceGroup : IMeasureBlockReader
    {
        private readonly IGraceGroupReader graceGroupReader;



        public Position Position => graceGroupReader.Target - graceGroupReader.ImplyDuration();

        public RythmicDuration RythmicDuration => graceGroupReader.ImplyRythmicDuration(RythmicDuration.QuarterNote);

        public Tuplet Tuplet => new (RythmicDuration, graceGroupReader.ReadChords().Select(c => graceGroupReader.ReadLayout().ChordDuration).ToArray());

        public int Id => graceGroupReader.Id;

        public int Length => graceGroupReader.Length;

        public Position Target => graceGroupReader.Target;

        public RythmicDuration ChordDuration => graceGroupReader.ReadLayout().ChordDuration;



        public MeasureBlockReaderFromGraceGroup(IGraceGroupReader graceGroupReader)
        {
            this.graceGroupReader = graceGroupReader;
        }

        public IEnumerable<IScoreElement> EnumerateChildren()
        {
            return graceGroupReader.EnumerateChildren();
        }

        public IEnumerable<IChordReader> ReadChords()
        {
            return graceGroupReader.ReadChords().Select(c => c.Cast(this));
        }

        public IMeasureBlockLayout ReadLayout()
        {
            return graceGroupReader.ReadLayout();
        }
    }
}
