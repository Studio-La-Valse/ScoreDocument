using StudioLaValse.ScoreDocument.Editor;
using StudioLaValse.ScoreDocument.Extensions;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;
using System.Collections;

namespace StudioLaValse.ScoreDocument.Private
{
    internal sealed class MeasureElementContainer : ScoreElement, IChordEditor, IChordReader
    {
        private readonly List<Note> measureElements;
        private readonly MeasureBlock hostBlock;
        private readonly IKeyGenerator<int> keyGenerator;
        private IMeasureElementContainerLayout layout;



        public RythmicDuration RythmicDuration { get; }



        public Tuplet Tuplet
        {
            get
            {
                var targetLength = hostBlock.Duration;
                var groupLength = hostBlock.ReadChords().Select(e => e.RythmicDuration).Sum();
                return new Tuplet(groupLength, targetLength);
            }
        }
        public Position Position
        {
            get
            {
                if (hostBlock.Grace)
                {
                    return hostBlock.Position;
                }

                var index = hostBlock.IndexOfOrThrow(this);
                var position = hostBlock.Position.ToPosition();

                foreach (var container in hostBlock.Containers.Take(index))
                {
                    position += container.ActualDuration();
                }

                return position;
            }
        }
        public int Voice =>
            hostBlock.Voice;
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
            layout = new MeasureElementContainerLayout();
        }



        public IRibbonMeasureReader ReadContext() =>
            hostBlock.RibbonMeasure;



        public void Clear()
        {
            measureElements.Clear();
        }
        public void Add(params Pitch[] pitches)
        {
            foreach (var pitch in pitches)
            {
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



        public IChordReader? ReadPrevious()
        {
            return hostBlock.ContainerLeft(this);
        }
        public IChordReader? ReadNext()
        {
            return hostBlock.ContainerRight(this);
        }





        public IMeasureElementContainerLayout ReadLayout()
        {
            return layout;
        }
        public void ApplyLayout(IMeasureElementContainerLayout layout)
        {
            this.layout = layout;
        }

    }
}
