using StudioLaValse.ScoreDocument.Extensions;
using StudioLaValse.ScoreDocument.Extensions.Private;
using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class MeasureBlockReaderFromGraceGroup : IMeasureBlock
    {
        private readonly IGraceGroup graceGroupReader;


        public Position Position => graceGroupReader.Target - graceGroupReader.ImplyDuration();

        public RythmicDuration RythmicDuration => graceGroupReader.ImplyRythmicDuration(RythmicDuration.QuarterNote);

        public Tuplet Tuplet => new(RythmicDuration, graceGroupReader.ReadChords().Select(c => graceGroupReader.BlockDuration).ToArray());

        public int Id => graceGroupReader.Id;

        public int Length => graceGroupReader.Length;

        public Position Target => graceGroupReader.Target;

        public RythmicDuration ChordDuration => graceGroupReader.BlockDuration;

        public StemDirection StemDirection
        {
            get => graceGroupReader.StemDirection;
            set => graceGroupReader.StemDirection = value;
        }

        public double StemLength
        {
            get => graceGroupReader.StemLength;
            set => graceGroupReader.StemLength = value;
        }

        public double BeamAngle
        {
            get => graceGroupReader.BeamAngle;
            set => graceGroupReader.BeamAngle = value;
        }

        public double BeamThickness
        {
            get => graceGroupReader.BeamThickness;
            set => graceGroupReader.BeamThickness = value;
        }

        public double BeamSpacing
        {
            get => graceGroupReader.BeamSpacing;
            set => graceGroupReader.BeamSpacing = value;
        }

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
            return graceGroupReader.ReadChords().Select(c => c.Imply(this));
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
            graceGroupReader.AppendChord(pitches);
        }

        public void Clear()
        {
            graceGroupReader.Clear();
        }

        public void Splice(int index)
        {
            graceGroupReader.Splice(index);
        }

        public void ResetStemDirection()
        {
            graceGroupReader.ResetStemDirection();
        }

        public void ResetStemLength()
        {
            graceGroupReader.ResetStemLength();
        }

        public void ResetBeamAngle()
        {
            graceGroupReader.ResetBeamAngle();
        }

        public void ResetBeamThickness()
        {
            graceGroupReader.ResetBeamThickness();
        }

        public void ResetBeamSpacing()
        {
            graceGroupReader.ResetBeamSpacing();
        }
    }
}
