using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Reader.Private
{
    internal class NoteReaderFromGraceChord : INoteReader
    {
        private readonly IGraceNoteReader noteReader;
        private readonly ChordReaderFromGraceChord graceChordReader;


        public Pitch Pitch => noteReader.Pitch;

        public Position Position => graceChordReader.Position;

        public RythmicDuration RythmicDuration => graceChordReader.RythmicDuration;

        public Tuplet Tuplet => graceChordReader.Tuplet;

        public int Id => noteReader.Id;

        public NoteReaderFromGraceChord(IGraceNoteReader noteReader, ChordReaderFromGraceChord graceChordReader)
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
    }
}
