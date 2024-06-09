using System.Linq;

namespace StudioLaValse.ScoreDocument.Implementation
{
    public interface IGraceTarget
    {
        Position Position { get; }
    }
    public sealed class GraceGroup : ScoreElement, IMementoElement<GraceGroupModel>
    {
        private readonly List<GraceChord> chords = [];
        private readonly IGraceTarget target;
        private readonly ScoreDocumentStyleTemplate styleTemplate;
        private readonly IKeyGenerator<int> keyGenerator;

        public IEnumerable<GraceChord> Chords => chords;
        public int Length => chords.Count;
        public Position Target => target.Position;

        public InstrumentMeasure HostMeasure { get; }
        public AuthorGraceGroupLayout AuthorLayout { get; }
        public UserGraceGroupLayout UserLayout { get; }

        public GraceGroup(
            IGraceTarget target,
            InstrumentMeasure hostMeasure,
            ScoreDocumentStyleTemplate styleTemplate, 
            AuthorGraceGroupLayout authorGraceGroupLayout,
            UserGraceGroupLayout userGraceGroupLayout,
            IKeyGenerator<int> keyGenerator, 
            Guid guid) : base(keyGenerator, guid)
        {
            this.styleTemplate = styleTemplate;
            this.keyGenerator = keyGenerator;
            this.target = target;
            HostMeasure = hostMeasure;
            AuthorLayout = authorGraceGroupLayout;
            UserLayout = userGraceGroupLayout;
        }

        public void Append(params Pitch[] pitches)
        {
            var chords = pitches.Select(pitch =>
            {
                var authorChordLayout = new AuthorGraceChordLayout(UserLayout);
                var userChordLayout = new UserGraceChordLayout(Guid.NewGuid(), UserLayout);
                var graceChord = new GraceChord(this, HostMeasure, authorChordLayout, userChordLayout, styleTemplate, keyGenerator, Guid.NewGuid());
                return graceChord;
            }).ToArray();

            Append(chords);
        }
        public void Append(params GraceChord[] chords)
        {
            foreach(var chord in chords)
            {
                this.chords.Add(chord);
            }

            Rebeam();
        }
        public void Splice(int index)
        {
            chords.RemoveAt(index);

            Rebeam();
        }  
        public void Clear()
        {
            chords.Clear();

            Rebeam();
        }
        public int IndexOfOrThrow(GraceChord chord)
        {
            var index = chords.IndexOf(chord);
            return index == -1 ? throw new Exception() : index; ;
        }


        public void Rebeam()
        {
            new RebeamStrategy().Rebeam(this.chords);
        }

        public GraceGroupModel GetMemento()
        {
            return new GraceGroupModel
            {
                Id = Guid,
                Chords = chords.Select(c => c.GetMemento()).ToList(),
                BeamAngle = AuthorLayout.BeamAngle,
                ChordDuration = AuthorLayout.ChordDuration.Convert(),
                ChordSpacing = AuthorLayout.ChordSpacing,
                OccupySpace = AuthorLayout.OccupySpace,
                StemLength = AuthorLayout.StemLength,
                Layout = UserLayout.GetMemento()
            };
        }
        public void ApplyMemento(GraceGroupModel memento)
        {
            Clear();

            AuthorLayout.ApplyMemento(memento);
            UserLayout.ApplyMemento(memento.Layout);

            foreach (var chordMemento in memento.Chords)
            {
                var authorChordLayout = new AuthorGraceChordLayout(UserLayout);
                var userChordLayout = new UserGraceChordLayout(chordMemento.Layout?.Id ?? Guid.NewGuid(), UserLayout);
                var graceChord = new GraceChord(this, HostMeasure, authorChordLayout, userChordLayout, styleTemplate, keyGenerator, chordMemento.Id);
                chords.Add(graceChord);
                graceChord.ApplyMemento(chordMemento);
            }
        }
    }
}
