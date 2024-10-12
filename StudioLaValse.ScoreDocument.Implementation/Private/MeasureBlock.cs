global using StudioLaValse.ScoreDocument.StyleTemplates;
using StudioLaValse.ScoreDocument.Implementation.Private.Interfaces;
using StudioLaValse.ScoreDocument.Implementation.Private.Layout;
using StudioLaValse.ScoreDocument.Implementation.Private.Memento;

namespace StudioLaValse.ScoreDocument.Implementation.Private
{
    internal class MeasureBlock : ScoreElement, IPositionElement, IMementoElement<MeasureBlockMemento>
    {
        private readonly List<Chord> chords;
        private readonly MeasureBlockChain host;
        private readonly ScoreDocumentStyleTemplate documentStyleTemplate;
        private readonly IKeyGenerator<int> keyGenerator;

        public Position Position
        {
            get
            {
                var index = host.IndexOfOrThrow(this);

                Position position = new(0, 4);

                foreach (var block in host.GetBlocksCore().Take(index))
                {
                    position += block.RythmicDuration;
                }

                return position;
            }
        }
        public InstrumentMeasure RibbonMeasure =>
            host.RibbonMeasure;
        public IEnumerable<Chord> Containers =>
            chords;
        public Tuplet Tuplet
        {
            get
            {
                var groupLength = Containers.Select(e => e.RythmicDuration).ToArray();
                return new Tuplet(RythmicDuration, groupLength);
            }
        }
        public int Voice => host.Voice;

        public RythmicDuration RythmicDuration { get; }
        public AuthorMeasureBlockLayout AuthorLayout { get; }
        public UserMeasureBlockLayout UserLayout { get; set; }
        public InstrumentMeasure InstrumentMeasure =>
            host.RibbonMeasure;

        public MeasureBlock(RythmicDuration duration,
                            MeasureBlockChain host,
                            ScoreDocumentStyleTemplate documentStyleTemplate,
                            AuthorMeasureBlockLayout layout,
                            UserMeasureBlockLayout secondaryLayout,
                            IKeyGenerator<int> keyGenerator,
                            Guid guid) : base(keyGenerator, guid)
        {
            this.host = host;
            this.documentStyleTemplate = documentStyleTemplate;
            this.keyGenerator = keyGenerator;

            chords = [];

            RythmicDuration = duration;
            AuthorLayout = layout;
            UserLayout = secondaryLayout;
        }



        public IEnumerable<Chord> GetChordsCore()
        {
            return chords;
        }


        public Chord? ContainerRight(Chord elementContainer)
        {
            var index = IndexOfOrThrow(elementContainer);
            return index - 1 < chords.Count ? chords[index + 1] : null;
        }
        public Chord? ContainerLeft(Chord elementContainer)
        {
            var index = IndexOfOrThrow(elementContainer);
            return index > 0 ? chords[index - 1] : null;
        }


        public int IndexOfOrThrow(Chord container)
        {
            var index = chords.IndexOf(container);

            return index == -1 ? throw new Exception("Measure element container does not exist in this measure block") : index;
        }

        public bool TryReadNext([NotNullWhen(true)] out MeasureBlock? right)
        {
            right = host.BlockRight(this);
            return right is not null;
        }
        public bool TryReadPrevious([NotNullWhen(true)] out MeasureBlock? previous)
        {
            previous = host.BlockLeft(this);
            return previous is not null;
        }





        public void Clear()
        {
            chords.Clear();
        }
        public void AppendChord(RythmicDuration rythmicDuration, bool rebeam = true, params Pitch[] pitches)
        {
            var beamtypes = new Dictionary<PowerOfTwo, BeamType>();

            var chordLayout = new AuthorChordLayout(documentStyleTemplate.MeasureBlockStyleTemplate, documentStyleTemplate.ChordStyleTemplate, beamtypes);
            var secondaryChordLayout = new UserChordLayout(chordLayout, Guid.NewGuid(), documentStyleTemplate.MeasureBlockStyleTemplate);

            var restLayout = new AuthorRestLayout(UserLayout, documentStyleTemplate.PageStyleTemplate);
            var userRestLayout = new UserRestLayout(UserLayout, restLayout, Guid.NewGuid());

            var chord = new Chord(this, rythmicDuration, documentStyleTemplate, chordLayout, secondaryChordLayout, restLayout, userRestLayout, beamtypes, keyGenerator, Guid.NewGuid());
            chord.Add(pitches);
            chords.Add(chord);
            if (rebeam)
            {
                Rebeam();
            }
        }
        public void Splice(int index)
        {
            chords.RemoveAt(index);
            Rebeam();
        }


        public void Divide(params int[] steps)
        {
            var stepsAsRythmicDurations = RythmicDuration.Divide(steps);

            Clear();
            foreach (var rythmicDuration in stepsAsRythmicDurations)
            {
                AppendChord(rythmicDuration, rebeam: false);
            }
            Rebeam();
        }
        public void DivideEqual(int number)
        {
            var stepsAsRythmicDurations = RythmicDuration.DivideEqual(number);

            Clear();
            foreach (var rythmicDuration in stepsAsRythmicDurations)
            {
                AppendChord(rythmicDuration, rebeam: false);
            }
            Rebeam();
        }



        public void Rebeam()
        {
            new RebeamStrategy().Rebeam(chords);
        }




        public MeasureBlockModel GetModel()
        {
            return new MeasureBlockModel()
            {
                Id = Guid,
                Chords = chords.Select(c => c.GetModel()).ToList(),
                RythmicDuration = RythmicDuration.Convert(),
                Voice = host.Voice,
                BeamAngle = AuthorLayout._BeamAngle.Field,
                StemLength = AuthorLayout._StemLength.Field,
                StemDirection = AuthorLayout._StemDirection.Field?.ConvertStemDirection(),
                Position = Position.Convert(),
                Scale = AuthorLayout._Scale.Field,
            };
        }
        public MeasureBlockLayoutModel GetLayoutModel()
        {
            return new MeasureBlockLayoutModel()
            {
                Id = UserLayout.Id,
                MeasureBlockId = Guid,
                StemLength = UserLayout._StemLength.Field,
                BeamAngle = UserLayout._BeamAngle.Field,
                StemDirection = UserLayout._StemDirection.Field?.ConvertStemDirection(),
                Scale = UserLayout._Scale.Field
            };
        }
        public MeasureBlockMemento GetMemento()
        {
            return new MeasureBlockMemento()
            {
                Id = Guid,
                Layout = GetLayoutModel(),
                Chords = chords.Select(c => c.GetMemento()).ToList(),
                RythmicDuration = RythmicDuration.Convert(),
                Voice = host.Voice,
                BeamAngle = AuthorLayout._BeamAngle.Field,
                StemLength = AuthorLayout._StemLength.Field,
                StemDirection = AuthorLayout._StemDirection.Field?.ConvertStemDirection(),
                Position = Position.Convert(),
                Scale = AuthorLayout._Scale.Field
            };
        }
        public void ApplyMemento(MeasureBlockMemento memento)
        {
            Clear();

            AuthorLayout.ApplyMemento(memento);

            foreach (var chordMemento in memento.Chords)
            {
                var beamtypes = new Dictionary<PowerOfTwo, BeamType>();
                var chordLayout = new AuthorChordLayout(documentStyleTemplate.MeasureBlockStyleTemplate, documentStyleTemplate.ChordStyleTemplate, beamtypes);
                var secondaryChordLayout = new UserChordLayout(chordLayout, Guid.NewGuid(), documentStyleTemplate.MeasureBlockStyleTemplate);

                var restLayout = new AuthorRestLayout(UserLayout, documentStyleTemplate.PageStyleTemplate);
                var userRestLayout = new UserRestLayout(UserLayout, restLayout, Guid.NewGuid());

                var chord = new Chord(this, chordMemento.RythmicDuration.Convert(), documentStyleTemplate, chordLayout, secondaryChordLayout, restLayout, userRestLayout, beamtypes, keyGenerator, chordMemento.Id);
                chords.Add(chord);
                chord.ApplyMemento(chordMemento);
            }

            Rebeam();

            var measureBlockLayoutModel = memento.Layout;
            UserLayout = new UserMeasureBlockLayout(measureBlockLayoutModel.Id, AuthorLayout, documentStyleTemplate.MeasureBlockStyleTemplate, host.RibbonMeasure.HostRibbon.UserLayout);
            UserLayout.ApplyMemento(measureBlockLayoutModel);
        }
    }
}


