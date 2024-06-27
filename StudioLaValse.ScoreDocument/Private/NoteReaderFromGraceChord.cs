using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class NoteReaderFromGraceChord : INote
    {
        private readonly IGraceNote noteReader;
        private readonly ChordReaderFromGraceChord graceChordReader;


        public Pitch Pitch
        {
            get => noteReader.Pitch;
            set => noteReader.Pitch = value;
        }

        public Position Position => graceChordReader.Position;

        public RythmicDuration RythmicDuration => graceChordReader.RythmicDuration;

        public Tuplet Tuplet => graceChordReader.Tuplet;

        public int Id => noteReader.Id;

        public AccidentalDisplay ForceAccidental
        {
            get => noteReader.ForceAccidental;
            set => noteReader.ForceAccidental = value;
        }
        public double Scale
        {
            get => noteReader.Scale;
            set => throw new NotImplementedException();
        }
        public int StaffIndex
        {
            get => noteReader.StaffIndex;
            set => noteReader.StaffIndex = value;
        }
        public double XOffset
        {
            get => noteReader.XOffset;
            set => throw new NotImplementedException();
        }

        public NoteReaderFromGraceChord(IGraceNote noteReader, ChordReaderFromGraceChord graceChordReader)
        {
            this.noteReader = noteReader;
            this.graceChordReader = graceChordReader;
        }

        public IEnumerable<IScoreElement> EnumerateChildren()
        {
            yield break;
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            if (other is null)
            {
                return false;
            }

            return other.Id == Id;
        }

        public void ResetAccidental()
        {
            noteReader.ResetAccidental();
        }

        public void ResetScale()
        {
            throw new NotImplementedException();
        }

        public void ResetStaffIndex()
        {
            noteReader.ResetStaffIndex();
        }

        public void ResetXOffset()
        {
            throw new NotImplementedException();
        }
    }
}
