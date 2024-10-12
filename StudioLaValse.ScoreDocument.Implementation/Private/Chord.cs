using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Implementation.Private.Layout;
using StudioLaValse.ScoreDocument.Implementation.Private.Memento;
using StudioLaValse.ScoreDocument.Models;

namespace StudioLaValse.ScoreDocument.Implementation.Private
{
    internal sealed class Chord : ScoreElement, IPositionElement, IMementoElement<ChordMemento>, IBeamEditor, IGraceTarget, IUniqueScoreElement
    {
        private readonly List<Note> measureElements;
        private readonly MeasureBlock hostBlock;
        private readonly ScoreDocumentStyleTemplate documentStyleTemplate;
        private readonly IKeyGenerator<int> keyGenerator;
        private readonly Dictionary<PowerOfTwo, BeamType> beamTypes;

        public Dictionary<PowerOfTwo, BeamType> BeamTypes => beamTypes;
        public RythmicDuration RythmicDuration { get; }
        public AuthorChordLayout AuthorLayout { get; }
        public UserChordLayout UserLayout { get; set; }
        public AuthorRestLayout AuthorRestLayout { get; }
        public UserRestLayout UserRestLayout { get; set; }



        public int Voice =>
            hostBlock.Voice;
        public Tuplet Tuplet =>
            hostBlock.Tuplet;
        public Position Position
        {
            get
            {
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
        public MeasureBlock HostBlock 
            => hostBlock;

        public GraceGroup? GraceGroup { get; set; }
        public int IndexInGroup => hostBlock.IndexOfOrThrow(this);

        public Chord(MeasureBlock hostBlock,
                     RythmicDuration displayDuration,
                     ScoreDocumentStyleTemplate documentStyleTemplate,
                     AuthorChordLayout chordLayout,
                     UserChordLayout secondaryChordLayout,
                     AuthorRestLayout restLayout,
                     UserRestLayout secondaryRestLayout,
                     Dictionary<PowerOfTwo, BeamType> beamTypes,
                     IKeyGenerator<int> keyGenerator,
                     Guid guid) : base(keyGenerator, guid)
        {
            this.hostBlock = hostBlock;
            this.keyGenerator = keyGenerator;
            this.documentStyleTemplate = documentStyleTemplate;
            this.beamTypes = beamTypes;

            measureElements = [];

            GraceGroup = null;

            RythmicDuration = displayDuration;
            AuthorLayout = chordLayout;
            UserLayout = secondaryChordLayout;
            AuthorRestLayout = restLayout;
            UserRestLayout = secondaryRestLayout;
        }





        public void Clear()
        {
            measureElements.Clear();
            AuthorLayout.Restore();
            UserLayout.Restore();
            GraceGroup?.Clear();
            GraceGroup = null;
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

                var noteLayout = new AuthorNoteLayout(documentStyleTemplate.PageStyleTemplate, hostBlock.UserLayout);
                var secondaryNoteLayout = new UserNoteLayout(Guid.NewGuid(), noteLayout, hostBlock.UserLayout);
                Note noteInMeasure = new(pitch, this, noteLayout, secondaryNoteLayout, keyGenerator, Guid.NewGuid());
                measureElements.Add(noteInMeasure);
            }
        }

        public void Set(params Pitch[] pitches)
        {
            measureElements.Clear();

            Add(pitches);
        }

        public void ApplyGrace(RythmicDuration rythmicDuration, params Pitch[] pitches)
        {
            // TODO: leave duration unset?
            var authorLayout = new AuthorGraceGroupLayout(documentStyleTemplate.GraceGroupStyleTemplate, Voice);
            authorLayout.ChordDuration.Value = rythmicDuration;
            var userLayout = new UserGraceGroupLayout(authorLayout, Guid.NewGuid(), documentStyleTemplate.GraceGroupStyleTemplate);
            var graceGroup = new GraceGroup(this, HostMeasure, documentStyleTemplate, authorLayout, userLayout, keyGenerator, Guid.NewGuid());
            graceGroup.Append(pitches);
            ApplyGrace(graceGroup);
        }

        public void ApplyGrace(GraceGroup graceGroup)
        {
            GraceGroup = graceGroup;
        }

        public IEnumerable<Note> EnumerateNotesCore()
        {
            return measureElements;
        }



        public ChordModel GetModel()
        {
            return new ChordModel
            {
                Id = Guid,
                Notes = measureElements.Select(n => n.GetModel()).ToList(),
                RythmicDuration = RythmicDuration.Convert(),
                GraceGroup = GraceGroup?.GetModel(),
                Position = Position.Convert(),
                SpaceRight = AuthorLayout._SpaceRight.Field
            };
        }

        public ChordLayoutModel GetLayoutModel()
        {
            return new ChordLayoutModel()
            {
                Id = UserLayout.Id,
                ChordId = Guid,
                SpaceRight = UserLayout._SpaceRight.Field,
            };
        }

        public ChordMemento GetMemento()
        {
            return new ChordMemento
            {
                Id = Guid,
                Layout = GetLayoutModel(),
                Notes = measureElements.Select(n => n.GetMemento()).ToList(),
                GraceGroup = GraceGroup?.GetMemento(),
                RythmicDuration = RythmicDuration.Convert(),
                Position = Position.Convert(),
                SpaceRight = AuthorLayout._SpaceRight.Field
            };
        }

        public void ApplyMemento(ChordMemento memento)
        {
            Clear();

            AuthorLayout.ApplyMemento(memento);

            foreach (var noteMemento in memento.Notes)
            {
                var pitch = noteMemento.Pitch.Convert();
                var noteLayout = new AuthorNoteLayout(documentStyleTemplate.PageStyleTemplate, hostBlock.UserLayout);
                var secondaryLayout = new UserNoteLayout(Guid.NewGuid(), noteLayout, hostBlock.UserLayout);
                var noteInMeasure = new Note(pitch, this, noteLayout, secondaryLayout, keyGenerator, noteMemento.Id);
                measureElements.Add(noteInMeasure);
                noteInMeasure.ApplyMemento(noteMemento);
            }

            GraceGroup = null;
            if (memento.GraceGroup is not null)
            {
                var authorLayout = new AuthorGraceGroupLayout(documentStyleTemplate.GraceGroupStyleTemplate, Voice);
                var userLayout = new UserGraceGroupLayout(authorLayout, Guid.NewGuid(), documentStyleTemplate.GraceGroupStyleTemplate);
                var graceGroup = new GraceGroup(this, HostMeasure, documentStyleTemplate, authorLayout, userLayout, keyGenerator, memento.GraceGroup.Id);
                GraceGroup = graceGroup;
                graceGroup.ApplyMemento(memento.GraceGroup);
            }

            var chordLayoutModel = memento.Layout;
            UserLayout = new UserChordLayout(AuthorLayout, chordLayoutModel.Id, documentStyleTemplate.MeasureBlockStyleTemplate);
            UserLayout.ApplyMemento(chordLayoutModel);
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
