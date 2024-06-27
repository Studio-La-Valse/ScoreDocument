using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Templates;

namespace StudioLaValse.ScoreDocument.Implementation
{
    public sealed class GraceChord : ScoreElement, IBeamEditor, IMementoElement<GraceChordModel>, IGraceTarget
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
                var timeRemainingInGroup = graceGroup.UserLayout.ChordDuration * nRemaining;
                var thisPosition = groupTarget - timeRemainingInGroup;
                return thisPosition;
            }
        }
        public int Voice => graceGroup.Voice;
        
        public GraceGroup? GraceGroup { get; set; } = null;

        public InstrumentMeasure HostMeasure { get; }
        public AuthorGraceChordLayout AuthorLayout { get; }
        public UserGraceChordLayout UserChordLayout { get; }


        public GraceChord(GraceGroup graceGroup, 
            InstrumentMeasure hostMeasure,
            AuthorGraceChordLayout graceChordLayout,
            UserGraceChordLayout userGraceChordLayout,
            ScoreDocumentStyleTemplate scoreDocumentStyleTemplate,
            IKeyGenerator<int> keyGenerator, 
            Guid guid) : base(keyGenerator, guid)
        {
            this.graceGroup = graceGroup;
            HostMeasure = hostMeasure;
            this.keyGenerator = keyGenerator;
            this.scoreDocumentStyleTemplate = scoreDocumentStyleTemplate;

            AuthorLayout = graceChordLayout;
            UserChordLayout = userGraceChordLayout;
        }

        public void Add(params Pitch[] pitches)
        {
            foreach(var pitch in pitches)
            {
                if (notes.Any(n => n.Pitch == pitch))
                {
                    continue;
                }

                var authorLayout = new AuthorGraceNoteLayout(graceGroup.UserLayout, scoreDocumentStyleTemplate.NoteStyleTemplate);
                var userLayout = new UserGraceNoteLayout(Guid.NewGuid(), graceGroup.UserLayout, scoreDocumentStyleTemplate.NoteStyleTemplate, authorLayout);
                var graceNote = new GraceNote(HostMeasure, authorLayout, userLayout, pitch, keyGenerator, Guid.NewGuid());
                Append(graceNote);
            }
        }
        public void Grace(params Pitch[] pitches)
        {
            var authorLayout = new AuthorGraceGroupLayout(scoreDocumentStyleTemplate.GraceGroupStyleTemplate, Voice);
            var userLayout = new UserGraceGroupLayout(authorLayout, Guid.NewGuid());
            var graceGroup = new GraceGroup(this, HostMeasure, scoreDocumentStyleTemplate, authorLayout, userLayout, keyGenerator, Guid.NewGuid());
            graceGroup.Append(pitches);
            ApplyGrace(graceGroup);
        }
        public void ApplyGrace(GraceGroup graceGroup)
        {
            GraceGroup = graceGroup;
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
            if(note is null)
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


        public GraceChordModel GetMemento()
        {
            return new GraceChordModel()
            {
                Id = Guid,
                Notes = notes.Select(n => n.GetMemento()).ToList(),
                IndexInGroup = IndexInGroup,
                Layout = UserChordLayout.GetMemento(),
                GraceGroup = GraceGroup?.GetMemento()
            };
        }
        public void ApplyMemento(GraceChordModel memento)
        {
            AuthorLayout.ApplyMemento(memento);
            UserChordLayout.ApplyMemento(memento.Layout);

            foreach(var note in memento.Notes)
            {
                var authorLayout = new AuthorGraceNoteLayout(graceGroup.UserLayout, scoreDocumentStyleTemplate.NoteStyleTemplate);
                var userLayout = new UserGraceNoteLayout(note.Layout?.Id ?? Guid.NewGuid(), graceGroup.UserLayout, scoreDocumentStyleTemplate.NoteStyleTemplate, authorLayout);
                var graceNote = new GraceNote(HostMeasure, authorLayout, userLayout, note.Pitch.Convert(), keyGenerator, note.Id);
                Append(graceNote);

                graceNote.ApplyMemento(note);
            }

            GraceGroup = null;
            if (memento.GraceGroup is not null)
            {
                var _authorLayout = new AuthorGraceGroupLayout(scoreDocumentStyleTemplate.GraceGroupStyleTemplate, Voice);
                var _userLayout = new UserGraceGroupLayout(_authorLayout, memento.Layout?.Id ?? Guid.NewGuid());
                var graceGroup = new GraceGroup(this, HostMeasure, scoreDocumentStyleTemplate, _authorLayout, _userLayout, keyGenerator, memento.GraceGroup.Id);
                GraceGroup = graceGroup; ;
                graceGroup.ApplyMemento(memento.GraceGroup);
            }
        }
    }
}
