using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Implementation.Private.Layout;
using StudioLaValse.ScoreDocument.Implementation.Private.Memento;

namespace StudioLaValse.ScoreDocument.Implementation.Private
{
    internal sealed class GraceChord : ScoreElement, IBeamEditor, IMementoElement<GraceChordMemento>, IGraceTarget, IUniqueScoreElement
    {
        private readonly List<GraceNote> notes = [];
        private readonly GraceGroup graceGroup;
        private readonly ScoreDocumentStyleTemplate scoreDocumentStyleTemplate;
        private readonly IKeyGenerator<int> keyGenerator;
        private readonly Dictionary<PowerOfTwo, BeamType> beamTypes = [];

        public RythmicDuration RythmicDuration =>
            graceGroup.UserLayout.ChordDuration;
        public Dictionary<PowerOfTwo, BeamType> BeamTypes =>
            beamTypes;
        public int IndexInGroup =>
            graceGroup.IndexOfOrThrow(this);
        public Position Position
        {
            get
            {
                var groupTarget = graceGroup.Target;
                var nRemaining = graceGroup.Length - graceGroup.IndexOfOrThrow(this);
                var timeRemainingInGroup = graceGroup.UserLayout.ChordDuration.Value * nRemaining;
                var thisPosition = groupTarget - timeRemainingInGroup;
                return thisPosition;
            }
        }
        public int Voice => graceGroup.Voice;


        public InstrumentMeasure HostMeasure { get; }
        public AuthorGraceChordLayout AuthorLayout { get; }
        public UserGraceChordLayout UserLayout { get; set; }

        public AuthorRestLayout AuthorRestLayout { get; }
        public UserRestLayout UserRestLayout { get; set; }



        public GraceChord(GraceGroup graceGroup,
            InstrumentMeasure hostMeasure,
            AuthorGraceChordLayout graceChordLayout,
            UserGraceChordLayout userGraceChordLayout,
            AuthorRestLayout restLayout,
            UserRestLayout userRestLayout,
            ScoreDocumentStyleTemplate scoreDocumentStyleTemplate,
            IKeyGenerator<int> keyGenerator,
            Guid guid) : base(keyGenerator, guid)
        {
            this.graceGroup = graceGroup;
            this.keyGenerator = keyGenerator;
            this.scoreDocumentStyleTemplate = scoreDocumentStyleTemplate;

            HostMeasure = hostMeasure;

            AuthorLayout = graceChordLayout;
            UserLayout = userGraceChordLayout;

            AuthorRestLayout = restLayout;
            UserRestLayout = userRestLayout;
        }

        public void Add(params Pitch[] pitches)
        {
            foreach (var pitch in pitches)
            {
                if (notes.Any(n => n.Pitch == pitch))
                {
                    continue;
                }

                var authorLayout = new AuthorGraceNoteLayout(graceGroup.UserLayout, scoreDocumentStyleTemplate.PageStyleTemplate);
                var userLayout = new UserGraceNoteLayout(Guid.NewGuid(), graceGroup.UserLayout, authorLayout);
                var graceNote = new GraceNote(HostMeasure, authorLayout, userLayout, scoreDocumentStyleTemplate, graceGroup, pitch, keyGenerator, Guid.NewGuid());
                Append(graceNote);
            }
        }
        public void Set(params Pitch[] pitches)
        {
            notes.Clear();

            Add(pitches);
        }
        public void Append(GraceNote graceNote)
        {
            notes.Add(graceNote);
        }
        public void Remove(Pitch pitch)
        {
            var note = notes.FirstOrDefault(n => n.Pitch == pitch);
            if (note is null)
            {
                //or throw....
                return;
            }

            notes.Remove(note);
        }
        public void Clear()
        {
            notes.Clear();
        }
        public IEnumerable<GraceNote> EnumerateNotes() => notes;


        public GraceChordModel GetModel()
        {
            return new GraceChordModel()
            {
                Id = Guid,
                Notes = notes.Select(n => n.GetModel()).ToList(),
                IndexInGroup = IndexInGroup,
            };
        }
        public GraceChordLayoutModel GetLayoutModel()
        {
            return new GraceChordLayoutModel()
            {
                Id = UserLayout.Guid,
                GraceChordId = Guid,
            };
        }
        public GraceChordMemento GetMemento()
        {
            return new GraceChordMemento()
            {
                Id = Guid,
                Layout = GetLayoutModel(),
                Notes = notes.Select(n => n.GetMemento()).ToList(),
                IndexInGroup = IndexInGroup,
            };
        }
        public void ApplyMemento(GraceChordMemento memento)
        {
            AuthorLayout.ApplyMemento(memento);

            foreach (var note in memento.Notes)
            {
                var authorLayout = new AuthorGraceNoteLayout(graceGroup.UserLayout, scoreDocumentStyleTemplate.PageStyleTemplate);
                var userLayout = new UserGraceNoteLayout(Guid.NewGuid(), graceGroup.UserLayout, authorLayout);
                var graceNote = new GraceNote(HostMeasure, authorLayout, userLayout, scoreDocumentStyleTemplate, graceGroup, note.Pitch.Convert(), keyGenerator, note.Id);
                Append(graceNote);

                graceNote.ApplyMemento(note);
            }

            var layoutModel = memento.Layout;
            UserLayout = new UserGraceChordLayout(layoutModel.Id, graceGroup.UserLayout, AuthorLayout.BeamTypes);
            UserLayout.ApplyMemento(layoutModel);
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
