using StudioLaValse.ScoreDocument.Layout;
namespace StudioLaValse.ScoreDocument.Private
{
    internal class ChordReaderFromGraceChord : IChord
    {
        private readonly IGraceChord graceChordReader;
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

        public ChordReaderFromGraceChord(IGraceChord graceChordReader, MeasureBlockReaderFromGraceGroup measureBlockReaderFromGraceGroup)
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

        public IGraceGroup? ReadGraceGroup()
        {
            return graceChordReader.ReadGraceGroup();
        }

        public IChordLayout ReadLayout()
        {
            return graceChordReader.ReadLayout();
        }

        public IEnumerable<INote> ReadNotes()
        {
            return graceChordReader.ReadNotes().Select(n => new NoteReaderFromGraceChord(n, this));
        }

        public bool Equals(IUniqueScoreElement? other)
        {
            if (other is null)
            {
                return false;
            }

            return other.Id == Id;
        }

        public void SetSpaceRight(double spaceRight)
        {
            throw new NotImplementedException();
        }

        public void SetXOffset(double offset)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Add(params Pitch[] pitches)
        {
            throw new NotImplementedException();
        }

        public void Set(params Pitch[] pitches)
        {
            throw new NotImplementedException();
        }

        public void RemoveLayout()
        {
            throw new NotImplementedException();
        }

        public void Grace(params Pitch[] pitches)
        {
            throw new NotImplementedException();
        }
    }
}
