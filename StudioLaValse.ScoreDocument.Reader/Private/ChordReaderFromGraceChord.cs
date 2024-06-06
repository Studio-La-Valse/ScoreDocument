using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader.Extensions;

namespace StudioLaValse.ScoreDocument.Reader.Private
{
    internal class ChordReaderFromGraceChord : IChordReader
    {
        private readonly IGraceChordReader graceChordReader;
        private readonly MeasureBlockReaderFromGraceGroup graceGroup;

        public Position Position
        {
            get
            {
                var nRemaining = graceGroup.Length - graceChordReader.IndexInGroup;
                var distanceToTarget = graceGroup.ChordDuration * nRemaining;
                var position = graceGroup.Target - distanceToTarget;
                return position;
            }
        }

        public RythmicDuration RythmicDuration => graceGroup.ChordDuration;

        public Tuplet Tuplet => graceGroup.Tuplet;

        public int Id => graceChordReader.Id;

        public ChordReaderFromGraceChord(IGraceChordReader graceChordReader, MeasureBlockReaderFromGraceGroup measureBlockReaderFromGraceGroup)
        {
            this.graceChordReader = graceChordReader;
            this.graceGroup = measureBlockReaderFromGraceGroup;
        }

        public IEnumerable<IScoreElement> EnumerateChildren()
        {
            return graceChordReader.EnumerateChildren();
        }

        public BeamType? ReadBeamType(PowerOfTwo i)
        {
            return graceChordReader.ReadBeamType(i);
        }

        public IEnumerable<KeyValuePair<PowerOfTwo, BeamType>> ReadBeamTypes()
        {
            return graceChordReader.ReadBeamTypes();
        }

        public IGraceGroupReader? ReadGraceGroup()
        {
            return graceChordReader.ReadGraceGroup();
        }

        public IChordLayout ReadLayout()
        {
            return graceChordReader.ReadLayout();
        }

        public IEnumerable<INoteReader> ReadNotes()
        {
            return graceChordReader.ReadNotes().Select(n => new NoteReaderFromGraceChord(n, this));
        }
    }
}
