using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Implementation
{
    public sealed class GraceNote : ScoreElement
    {
        private readonly GraceChord graceChord;

        public Pitch Pitch { get; set; }


        public GraceNote(GraceChord graceChord, Pitch pitch, IKeyGenerator<int> keyGenerator, Guid guid) : base(keyGenerator, guid)
        {
            this.graceChord = graceChord;
            
            Pitch = pitch;
        }
    }
}
