﻿namespace StudioLaValse.ScoreDocument.Implementation
{
    public class MeasureBlock : ScoreElement, IPositionElement, IMementoElement<MeasureBlockModel>
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
                    if (block.Grace)
                    {
                        continue;
                    }

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


        public bool Grace { get; }
        public RythmicDuration RythmicDuration { get; }
        public AuthorMeasureBlockLayout AuthorLayout { get; }
        public UserMeasureBlockLayout UserLayout { get; }
        public InstrumentMeasure InstrumentMeasure =>
            host.RibbonMeasure;

        public MeasureBlock(RythmicDuration duration,
                            MeasureBlockChain host,
                            ScoreDocumentStyleTemplate documentStyleTemplate,
                            AuthorMeasureBlockLayout layout,
                            UserMeasureBlockLayout secondaryLayout,
                            bool grace,
                            IKeyGenerator<int> keyGenerator,
                            Guid guid) : base(keyGenerator, guid)
        {
            this.host = host;
            this.documentStyleTemplate = documentStyleTemplate;
            this.keyGenerator = keyGenerator;

            chords = [];

            Grace = grace;
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
        public void AppendChord(RythmicDuration rythmicDuration, bool rebeam = true)
        {
            var guid = Guid.NewGuid();
            var layoutGuid = Guid.NewGuid();
            var chordLayout = new AuthorChordLayout(documentStyleTemplate.ChordStyleTemplate);
            var secondaryChordLayout = new UserChordLayout(chordLayout, layoutGuid);
            var chord = new Chord(this, rythmicDuration, documentStyleTemplate, chordLayout, secondaryChordLayout, keyGenerator, guid);
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




        public MeasureBlockModel GetMemento()
        {
            return new MeasureBlockModel()
            {
                Id = Guid,
                Chords = chords.Select(c => c.GetMemento()).ToList(),
                Duration = RythmicDuration.Convert(),
                Layout = UserLayout.GetMemento(),
                Voice = host.Voice,
                BeamAngle = AuthorLayout._BeamAngle.Field,
                StemLength = AuthorLayout._StemLength.Field,
                Position = Position.Convert()
            };
        }

        public void ApplyMemento(MeasureBlockModel memento)
        {
            Clear();

            AuthorLayout.ApplyMemento(memento);
            UserLayout.ApplyMemento(memento.Layout);

            foreach (var chordMemento in memento.Chords)
            {
                var chordLayout = new AuthorChordLayout(documentStyleTemplate.ChordStyleTemplate);
                var secondaryChordLayout = new UserChordLayout(chordLayout, chordMemento.Layout?.Id ?? Guid.NewGuid());
                var chord = new Chord(this, chordMemento.RythmicDuration.Convert(), documentStyleTemplate, chordLayout, secondaryChordLayout, keyGenerator, chordMemento.Id);
                chords.Add(chord);
                chord.ApplyMemento(chordMemento);
            }

            Rebeam();
        }
    }
}


