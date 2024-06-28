using StudioLaValse.ScoreDocument.Extensions;
using StudioLaValse.ScoreDocument.Extensions.Private;
using StudioLaValse.ScoreDocument.Layout;
using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class MeasureBlockReaderFromGraceGroup : IMeasureBlock
    {
        private readonly IGraceGroup graceGroupReader;


        public Position Position => graceGroupReader.Target - graceGroupReader.ImplyDuration();

        public RythmicDuration RythmicDuration => graceGroupReader.ImplyRythmicDuration(RythmicDuration.QuarterNote);

        public Tuplet Tuplet => new(RythmicDuration, graceGroupReader.ReadChords().Select(c => graceGroupReader.BlockDuration.Value).ToArray());

        public int Id => graceGroupReader.Id;

        public int Length => graceGroupReader.Length;

        public Position Target => graceGroupReader.Target;

        public RythmicDuration ChordDuration => graceGroupReader.BlockDuration;

        public TemplateProperty<StemDirection> StemDirection => graceGroupReader.StemDirection;

        public TemplateProperty<double> StemLength => graceGroupReader.StemLength;

        public TemplateProperty<double> BeamAngle => graceGroupReader.BeamAngle;

        public ReadonlyTemplateProperty<double> BeamSpacing => new ReadonlyTemplatePropertyFromFunc<double>(() => graceGroupReader.BeamSpacing.Value);

        public ReadonlyTemplateProperty<double> BeamThickness => new ReadonlyTemplatePropertyFromFunc<double>(() => graceGroupReader.BeamThickness.Value);




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

        public bool TryReadNext([NotNullWhen(true)] out IMeasureBlock? right)
        {
            throw new NotImplementedException();
        }

        public bool TryReadPrevious([NotNullWhen(true)] out IMeasureBlock? left)
        {
            throw new NotImplementedException();
        }

        public void Restore()
        {
            graceGroupReader.Restore();
        }
    }
}
