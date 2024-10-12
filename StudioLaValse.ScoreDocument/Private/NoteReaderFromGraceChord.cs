using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.StyleTemplates;

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

        public TemplateProperty<int> StaffIndex => noteReader.StaffIndex;

        public TemplateProperty<AccidentalDisplay> ForceAccidental => noteReader.ForceAccidental;

        public TemplateProperty<ColorARGB> Color => noteReader.Color;

        public ReadonlyTemplateProperty<double> Scale => noteReader.Scale;


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
    }
}
