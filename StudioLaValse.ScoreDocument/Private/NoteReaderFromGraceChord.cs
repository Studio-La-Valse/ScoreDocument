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

        public NoteReaderFromGraceChord(IGraceNote noteReader, ChordReaderFromGraceChord graceChordReader)
        {
            this.noteReader = noteReader;
            this.graceChordReader = graceChordReader;
        }

        public IEnumerable<IScoreElement> EnumerateChildren()
        {
            yield break;
        }

        public INoteLayout ReadLayout()
        {
            return noteReader.ReadLayout();
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            if (other is null)
            {
                return false;
            }

            return other.Id == Id;
        }

        public void SetStaffIndex(int staffIndex)
        {
            throw new NotImplementedException();
        }

        public void SetXOffset(double offset)
        {
            throw new NotImplementedException();
        }

        public void SetForceAccidental(AccidentalDisplay display)
        {
            throw new NotImplementedException();
        }

        public void SetScale(double scale)
        {
            throw new NotImplementedException();
        }

        public void RemoveLayout()
        {
            throw new NotImplementedException();
        }
    }
}
