namespace StudioLaValse.ScoreDocument.Private
{
    internal class Note : ScoreElement, INoteEditor, INoteReader
    {
        private readonly MeasureElementContainer container;
        private IMeasureElementLayout layout;



        public Pitch Pitch { get; set; }



        public int Voice =>
            container.Voice;
        public bool Grace =>
            container.Grace;
        public Position Position =>
            container.Position;
        public RythmicDuration RythmicDuration =>
            container.RythmicDuration;
        public Tuplet Tuplet =>
            container.Tuplet;


        internal Note(Pitch pitch, MeasureElementContainer container, IKeyGenerator<int> keyGenerator) : base(keyGenerator)
        {
            Pitch = pitch;
            this.container = container;
            this.layout = new MeasureElementLayout();
        }




        public IChordReader ReadContext() =>
            container;



        public IMeasureElementLayout ReadLayout()
        {
            return layout;
        }
        public void ApplyLayout(IMeasureElementLayout layout)
        {
            this.layout = layout;
        }
    }
}