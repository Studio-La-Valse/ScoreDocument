namespace StudioLaValse.ScoreDocument.Implementation
{
    public class GraceChordLayout : IChordLayout
    {
        public double XOffset => throw new NotImplementedException();

        public double SpaceRight => throw new NotImplementedException();
    }

    public sealed class GraceChord : ScoreElement, IBeamEditor
    {
        private readonly List<GraceNote> notes = [];
        private readonly GraceGroup graceGroup;
        private readonly IKeyGenerator<int> keyGenerator;
        private readonly Dictionary<PowerOfTwo, BeamType> beamTypes = [];

        public IEnumerable<GraceNote> Notes => notes;

        public RythmicDuration RythmicDuration => graceGroup.Layout.ChordDuration;

        public Dictionary<PowerOfTwo, BeamType> BeamTypes => beamTypes;

        public GraceGroup? GraceGroup { get; set; } = null;

        public GraceChord(GraceGroup graceGroup, IKeyGenerator<int> keyGenerator, Guid guid) : base(keyGenerator, guid)
        {
            this.graceGroup = graceGroup;
            this.keyGenerator = keyGenerator;
        }

        public void Append(Pitch pitch)
        {
            if(notes.Any(n => n.Pitch == pitch))
            {
                return;
            }

            notes.Add(new GraceNote(this, pitch, keyGenerator, Guid.NewGuid()));
        }
        public void Remove(Pitch pitch)
        {
            var note = notes.FirstOrDefault(n => n.Pitch == pitch);
            if(note is null)
            {
                //or throw....
                return;
            }

            notes.Remove(note);
        }
    }
}
