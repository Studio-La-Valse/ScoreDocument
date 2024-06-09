using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Primitives;
using System.ComponentModel.DataAnnotations;

namespace StudioLaValse.ScoreDocument.Implementation
{
    public sealed class Chord : ScoreElement, IPositionElement, IMementoElement<ChordModel>, IBeamEditor, IGraceTarget
    {
        private readonly List<Note> measureElements;
        private readonly MeasureBlock hostBlock;
        private readonly ScoreDocumentStyleTemplate documentStyleTemplate;
        private readonly IKeyGenerator<int> keyGenerator;
        private readonly Dictionary<PowerOfTwo, BeamType> beamTypes = [];

        public Dictionary<PowerOfTwo, BeamType> BeamTypes => beamTypes;
        public RythmicDuration RythmicDuration { get; }
        public AuthorChordLayout AuthorLayout { get; }
        public UserChordLayout UserLayout { get; }

        public Tuplet Tuplet =>
            hostBlock.Tuplet;
        public Position Position
        {
            get
            {
                if (hostBlock.Grace)
                {
                    return hostBlock.Position;
                }

                var position = hostBlock.Position;

                foreach (var container in hostBlock.Containers.Take(IndexInGroup))
                {
                    position += container.ActualDuration();
                }

                return position;
            }
        }
        public InstrumentMeasure HostMeasure =>
            hostBlock.RibbonMeasure;


        public GraceGroup? GraceGroup { get; set; }
        public int IndexInGroup => hostBlock.IndexOfOrThrow(this);

        public Chord(MeasureBlock hostBlock,
                     RythmicDuration displayDuration,
                     ScoreDocumentStyleTemplate documentStyleTemplate,
                     AuthorChordLayout chordLayout,
                     UserChordLayout secondaryChordLayout,
                     IKeyGenerator<int> keyGenerator,
                     Guid guid) : base(keyGenerator, guid)
        {
            this.hostBlock = hostBlock;
            this.keyGenerator = keyGenerator;
            this.documentStyleTemplate = documentStyleTemplate;

            measureElements = [];

            GraceGroup = null;

            RythmicDuration = displayDuration;
            AuthorLayout = chordLayout;
            UserLayout = secondaryChordLayout;
        }





        public void Clear()
        {
            measureElements.Clear();
            AuthorLayout.Restore();
            UserLayout.Restore();
        }
        public void Add(params Pitch[] pitches)
        {
            foreach (var pitch in pitches)
            {
                if (measureElements.Any(e => e.Pitch == pitch))
                {
                    //or throw....
                    continue;
                }

                var noteLayout = new AuthorNoteLayout(documentStyleTemplate.NoteStyleTemplate);
                var secondaryNoteLayout = new UserNoteLayout(Guid.NewGuid(), noteLayout);
                Note noteInMeasure = new(pitch, this, noteLayout, secondaryNoteLayout, keyGenerator, Guid.NewGuid());
                measureElements.Add(noteInMeasure);
            }
        }
        public void Set(params Pitch[] pitches)
        {
            measureElements.Clear();

            Add(pitches);
        }



        public IEnumerable<Note> EnumerateNotesCore()
        {
            return measureElements;
        }



        public ChordModel GetMemento()
        {
            return new ChordModel
            {
                Id = Guid,
                Notes = measureElements.Select(n => n.GetMemento()).ToList(),
                RythmicDuration = RythmicDuration.Convert(),
                Layout = UserLayout.GetMemento(),
                GraceGroup = GraceGroup?.GetMemento(),
                XOffset = AuthorLayout._XOffset.Field,
                SpaceRight = AuthorLayout._SpaceRight.Field,
                Position = Position.Convert()
            };
        }
        public void ApplyMemento(ChordModel memento)
        {
            Clear();

            AuthorLayout.ApplyMemento(memento);
            UserLayout.ApplyMemento(memento.Layout);

            foreach (var noteMemento in memento.Notes)
            {
                var pitch = noteMemento.Pitch.Convert();
                var noteLayout = new AuthorNoteLayout(documentStyleTemplate.NoteStyleTemplate);
                var secondaryLayout = new UserNoteLayout(noteMemento.Layout?.Id ?? Guid.NewGuid(), noteLayout);
                var noteInMeasure = new Note(pitch, this, noteLayout, secondaryLayout, keyGenerator, noteMemento.Id);
                measureElements.Add(noteInMeasure);
                noteInMeasure.ApplyMemento(noteMemento);
            }

            GraceGroup = null;
            if(memento.GraceGroup is not null)
            {
                var authorLayout = new AuthorGraceGroupLayout(documentStyleTemplate.GraceGroupStyleTemplate);
                var userLayout = new UserGraceGroupLayout(authorLayout, memento.Layout?.Id ?? Guid.NewGuid());
                var graceGroup = new GraceGroup(this, HostMeasure, documentStyleTemplate, authorLayout, userLayout, keyGenerator, memento.GraceGroup.Id);
                GraceGroup = graceGroup; ;
                graceGroup.ApplyMemento(memento.GraceGroup);
            }
        }


        public void ApplyGrace(params Pitch[] pitches)
        {
            var authorLayout = new AuthorGraceGroupLayout(documentStyleTemplate.GraceGroupStyleTemplate);
            var userLayout = new UserGraceGroupLayout(authorLayout, Guid.NewGuid());
            var graceGroup = new GraceGroup(this, HostMeasure, documentStyleTemplate, authorLayout, userLayout, keyGenerator, Guid.NewGuid());
            graceGroup.Append(pitches);
            ApplyGrace(graceGroup);
        }
        public void ApplyGrace(GraceGroup graceGroup)
        {
            GraceGroup = graceGroup;
        }


        public BeamType? GetBeamType(PowerOfTwo i)
        {
            return beamTypes.TryGetValue(i, out var value) ? value : null;
        }
        public IEnumerable<KeyValuePair<PowerOfTwo, BeamType>> GetBeamTypes()
        {
            return beamTypes;
        }
    }
}
