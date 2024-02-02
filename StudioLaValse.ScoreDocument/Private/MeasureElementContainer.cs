using StudioLaValse.ScoreDocument.Extensions;

namespace StudioLaValse.ScoreDocument.Private
{
    internal sealed class MeasureElementContainer : ScoreElement, IChordEditor, IChordReader, IPositionElement
    {
        private readonly List<Note> measureElements;
        private readonly MeasureBlock hostBlock;
        private readonly IKeyGenerator<int> keyGenerator;
        private IChordLayout layout;



        public RythmicDuration RythmicDuration { get; }



        public Tuplet Tuplet => hostBlock.Tuplet;
        public Position Position
        {
            get
            {
                if (hostBlock.Grace)
                {
                    return hostBlock.Position;
                }

                var index = hostBlock.IndexOfOrThrow(this);
                var position = hostBlock.Position;

                foreach (var container in hostBlock.Containers.Take(index))
                {
                    position += container.ActualDuration();
                }

                return position;
            }
        }
        public bool Grace =>
            hostBlock.Grace;
        public int IndexInBlock =>
            hostBlock.IndexOfOrThrow(this);



        public MeasureElementContainer(MeasureBlock hostBlock, RythmicDuration displayDuration, IKeyGenerator<int> keyGenerator) : base(keyGenerator)
        {
            this.hostBlock = hostBlock;
            this.keyGenerator = keyGenerator;
            measureElements = [];
            RythmicDuration = displayDuration;
            layout = new ChordLayout();
        }



        public IInstrumentMeasureReader ReadContext() =>
            hostBlock.RibbonMeasure;



        public void Clear()
        {
            measureElements.Clear();
        }
        public void Add(params Pitch[] pitches)
        {
            foreach (var pitch in pitches)
            {
                if(measureElements.Any(e => e.Pitch == pitch))
                {
                    continue;
                }

                var noteInMeasure = new Note(pitch, this, keyGenerator);
                measureElements.Add(noteInMeasure);
            }
        }
        public void Set(params Pitch[] pitches)
        {
            measureElements.Clear();

            Add(pitches);
        }



        public IEnumerable<INote> EnumerateNotes()
        {
            return measureElements;
        }
        public IEnumerable<INoteReader> ReadNotes()
        {
            return measureElements;
        }
        public IEnumerable<INoteEditor> EditNotes()
        {
            return measureElements;
        }




        public IChordLayout ReadLayout()
        {
            return layout;
        }
        public void ApplyLayout(IChordLayout layout)
        {
            this.layout = layout;
        }

    }
}
