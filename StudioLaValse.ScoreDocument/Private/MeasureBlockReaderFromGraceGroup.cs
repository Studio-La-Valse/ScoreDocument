using StudioLaValse.ScoreDocument.Extensions;
using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class MeasureBlockReaderFromGraceGroup : IMeasureBlock
    {
        private readonly IGraceGroup graceGroupReader;



        public Position Position => graceGroupReader.Target - graceGroupReader.ImplyDuration();

        public RythmicDuration RythmicDuration => graceGroupReader.ImplyRythmicDuration(RythmicDuration.QuarterNote);

        public Tuplet Tuplet => new(RythmicDuration, graceGroupReader.ReadChords().Select(c => graceGroupReader.ReadLayout().ChordDuration).ToArray());

        public int Id => graceGroupReader.Id;

        public int Length => graceGroupReader.Length;

        public Position Target => graceGroupReader.Target;

        public RythmicDuration ChordDuration => graceGroupReader.ReadLayout().ChordDuration;



        public MeasureBlockReaderFromGraceGroup(IGraceGroup graceGroupReader)
        {
            this.graceGroupReader = graceGroupReader;
        }

        public IEnumerable<IScoreElement> EnumerateChildren()
        {
            return graceGroupReader.EnumerateChildren();
        }

        public IEnumerable<IChord> ReadChords()
        {
            return graceGroupReader.ReadChords().Select(c => c.Cast(this));
        }

        public IMeasureBlockLayout ReadLayout()
        {
            return graceGroupReader.ReadLayout();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            if (other is null)
            {
                return false;
            }

            return other.Id == Id;
        }

        public void AppendChord(RythmicDuration rythmicDuration, params Pitch[] pitches)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Splice(int index)
        {
            throw new NotImplementedException();
        }

        public void SetStemLength(double stemLength)
        {
            throw new NotImplementedException();
        }

        public void SetBeamAngle(double angle)
        {
            throw new NotImplementedException();
        }

        public void RemoveLayout()
        {
            throw new NotImplementedException();
        }
    }
}
