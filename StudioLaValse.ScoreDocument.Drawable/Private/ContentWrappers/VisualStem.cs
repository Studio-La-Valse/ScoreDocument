namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal sealed class VisualStem : BaseContentWrapper
    {
        private static readonly double thickness = 0.08;
        private readonly ColorARGB color;

        public bool VisuallyUp
        {
            get
            {
                return End.Y < Origin.Y;
            }
        }

        public XY Origin { get; }
        public XY End { get; }
        public IChordReader Chord { get; }




        public VisualStem(XY origin, XY end, IChordReader chord, ColorARGB color)
        {
            this.color = color;
            Origin = origin;
            End = end;
            Chord = chord;
        }



        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            yield return new DrawableLine(Origin, End, color: color, thickness: thickness);
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            yield break;
        }
    }
}
