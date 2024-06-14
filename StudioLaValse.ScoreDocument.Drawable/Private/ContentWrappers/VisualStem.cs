using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal sealed class VisualStem : BaseContentWrapper
    {
        private readonly double thickness;
        private readonly IScoreDocumentLayout scoreDocumentLayout;


        public XY Origin { get; }
        public XY End { get; }
        public IChordReader Chord { get; }

        public double Thickness => this.thickness;
        public bool VisuallyUp => End.Y < Origin.Y;

        public VisualStem(XY origin, XY end, double thickness, IChordReader chord, IScoreDocumentLayout scoreDocumentLayout)
        {
            this.thickness = thickness;
            this.scoreDocumentLayout = scoreDocumentLayout;

            Origin = origin;
            End = end;
            Chord = chord;
        }



        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            yield return new DrawableLine(Origin, End, color: scoreDocumentLayout.PageForegroundColor.FromPrimitive(), Thickness);
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            yield break;
        }
    }
}
