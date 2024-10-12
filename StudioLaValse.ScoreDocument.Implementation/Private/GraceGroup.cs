using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Implementation.Private.Layout;
using StudioLaValse.ScoreDocument.Implementation.Private.Memento;

namespace StudioLaValse.ScoreDocument.Implementation.Private
{
    internal sealed class GraceGroup : ScoreElement, IMementoElement<GraceGroupMemento>
    {
        private readonly List<GraceChord> chords = [];
        private readonly IGraceTarget target;
        private readonly ScoreDocumentStyleTemplate styleTemplate;
        private readonly IKeyGenerator<int> keyGenerator;

        public IEnumerable<GraceChord> Chords => chords;
        public int Length => chords.Count;
        public int Voice => target.Voice;
        public Position Target => target.Position;

        public InstrumentMeasure HostMeasure { get; }
        public AuthorGraceGroupLayout AuthorLayout { get; }
        public UserGraceGroupLayout UserLayout { get; set; }

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
                var beamTypes = new Dictionary<PowerOfTwo, BeamType>();
                var authorChordLayout = new AuthorGraceChordLayout(UserLayout, beamTypes);
                var userChordLayout = new UserGraceChordLayout(Guid.NewGuid(), UserLayout, beamTypes);

                var authorRestLayout = new AuthorRestLayout(UserLayout, styleTemplate.PageStyleTemplate);
                var userRestLayout = new UserRestLayout(UserLayout, authorRestLayout, Guid.NewGuid());
                
                var graceChord = new GraceChord(this, HostMeasure, authorChordLayout, userChordLayout, authorRestLayout, userRestLayout, styleTemplate, keyGenerator, Guid.NewGuid());
                return graceChord;
            }).ToArray();

            Append(chords);
        }
        public void Append(params GraceChord[] chords)
        {
            foreach (var chord in chords)
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
            new RebeamStrategy().Rebeam(chords);
        }

        public GraceGroupModel GetModel()
        {
            return new GraceGroupModel
            {
                Id = Guid,
                Chords = chords.Select(c => c.GetModel()).ToList(),
                BeamAngle = AuthorLayout.BeamAngle,
                ChordDuration = AuthorLayout._ChordDuration.Field?.Convert(),
                ChordSpacing = AuthorLayout._ChordSpacing.Field,
                OccupySpace = AuthorLayout._OccupySpace.Field,
                StemLength = AuthorLayout._StemLength.Field,
                StemDirection = AuthorLayout._StemDirection.Field?.ConvertStemDirection(),
                Scale = AuthorLayout._Scale.Field,
            };
        }
        public GraceGroupLayoutModel GetLayoutModel()
        {
            return new GraceGroupLayoutModel()
            {
                Id = UserLayout.Guid,
                GraceGroupId = Guid,
                BeamAngle = UserLayout.BeamAngle,
                ChordDuration = UserLayout._ChordDuration.Field?.Convert(),
                ChordSpacing = UserLayout._ChordSpacing.Field,
                OccupySpace = UserLayout._OccupySpace.Field,
                StemLength = UserLayout._StemLength.Field,
                StemDirection = UserLayout._StemDirection.Field?.ConvertStemDirection(),
                Scale = UserLayout._Scale.Field,
            };
        }
        public GraceGroupMemento GetMemento()
        {
            return new GraceGroupMemento
            {
                Id = Guid,
                Layout = GetLayoutModel(),
                Chords = chords.Select(c => c.GetMemento()).ToList(),
                BeamAngle = AuthorLayout.BeamAngle,
                ChordDuration = AuthorLayout._ChordDuration.Field?.Convert(),
                ChordSpacing = AuthorLayout._ChordSpacing.Field,
                OccupySpace = AuthorLayout._OccupySpace.Field,
                StemLength = AuthorLayout._StemLength.Field,
                StemDirection = AuthorLayout._StemDirection.Field?.ConvertStemDirection(),
                Scale = AuthorLayout._Scale.Field,
            };
        }
        public void ApplyMemento(GraceGroupMemento memento)
        {
            Clear();

            AuthorLayout.ApplyMemento(memento);

            foreach (var chordMemento in memento.Chords)
            {
                var beamTypes = new Dictionary<PowerOfTwo, BeamType>();
                var authorChordLayout = new AuthorGraceChordLayout(UserLayout, beamTypes);
                var userChordLayout = new UserGraceChordLayout(Guid.NewGuid(), UserLayout, beamTypes);

                var authorRestLayout = new AuthorRestLayout(UserLayout, styleTemplate.PageStyleTemplate);
                var userRestLayout = new UserRestLayout(UserLayout, authorRestLayout, Guid.NewGuid());

                var graceChord = new GraceChord(this, HostMeasure, authorChordLayout, userChordLayout, authorRestLayout, userRestLayout, styleTemplate, keyGenerator, chordMemento.Id);
                chords.Add(graceChord);
                graceChord.ApplyMemento(chordMemento);
            }

            var layoutMemento = memento.Layout;
            UserLayout = new UserGraceGroupLayout(AuthorLayout, layoutMemento.Id, styleTemplate.GraceGroupStyleTemplate);
            UserLayout.ApplyMemento(layoutMemento);
        }
    }
}
