namespace StudioLaValse.ScoreDocument.Implementation
{
    public sealed class GraceGroup : ScoreElement
    {
        private readonly List<GraceChord> chords = [];
        private readonly Chord chord;
        private readonly IKeyGenerator<int> keyGenerator;

        public IEnumerable<GraceChord> Chords => chords;
        public Position Target => chord.Position;
        public int Length => chords.Count;

        public GraceGroupLayout Layout { get; } = new();


        public GraceGroup(Chord chord, IKeyGenerator<int> keyGenerator, Guid guid) : base(keyGenerator, guid)
        {
            this.chord = chord;
            this.keyGenerator = keyGenerator;
        }

        public void Append(params Pitch[] pitches)
        {
            var chord = new GraceChord(this, keyGenerator, Guid.NewGuid());
            foreach(var pitch in pitches)
            {
                chord.Append(pitch);
            }
            chords.Add(chord);
        }
        public void Remove(int index)
        {
            chords.RemoveAt(index);
        }  
        public void Clear()
        {
            chords.Clear();
        }
        public int IndexOfOrThrow(GraceChord chord)
        {
            var index = chords.IndexOf(chord);
            return index == -1 ? throw new Exception() : index; ;
        }
    }
}
