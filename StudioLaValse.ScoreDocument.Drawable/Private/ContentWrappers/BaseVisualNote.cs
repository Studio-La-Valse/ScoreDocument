namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal abstract class BaseVisualNote : BaseVisualParent<IUniqueScoreElement>
    {
        protected readonly IPositionElement measureElement;


        public abstract DrawableScoreGlyph Glyph { get; }
        public abstract bool OffsetDots { get; }
        public abstract double Scale { get; }
        public abstract ColorARGB Color { get; }
        public double CanvasLeft { get; }
        public double CanvasTop { get; }



        public RythmicDuration DisplayDuration =>
            measureElement.RythmicDuration;
        public IEnumerable<DrawableCircle> Dots
        {
            get
            {
                var lineSpacingOnStaff = GlyphLibrary.Glyph.LineSpacing * Scale;
                var heightOnCanvas = CanvasTop;
                if (OffsetDots)
                {
                    heightOnCanvas -= lineSpacingOnStaff / 2;
                }

                var spacing = 3.5 * (GlyphLibrary.Glyph.Em / 12) * Scale;
                var startLeft = CanvasLeft + spacing;

                for (var i = 0; i < measureElement.RythmicDuration.Dots; i++)
                {
                    DrawableCircle circle = new(
                        new XY(startLeft, heightOnCanvas),
                        lineSpacingOnStaff / 6,
                        Color);

                    yield return circle;

                    startLeft += spacing;
                }
            }
        }




        public BaseVisualNote(INote measureElement,
                              double canvasLeft,
                              double canvasTop) : base(measureElement)
        {
            this.measureElement = measureElement;
            CanvasLeft = canvasLeft;
            CanvasTop = canvasTop;
        }
        public BaseVisualNote(IChord measureElement,
                              double canvasLeft,
                              double canvasTop) : base(measureElement)
        {
            this.measureElement = measureElement;
            CanvasLeft = canvasLeft;
            CanvasTop = canvasTop;
        }


        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            yield return Glyph;

            foreach (var dot in Dots)
            {
                yield return dot;
            }
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            yield break;
        }
        public override BoundingBox BoundingBox()
        {
            return new BoundingBox(
                CanvasLeft - GlyphLibrary.Glyph.LineSpacing * Scale / 2,
                CanvasLeft + GlyphLibrary.Glyph.LineSpacing * Scale / 2,
                CanvasTop - GlyphLibrary.Glyph.LineSpacing * Scale / 2,
                CanvasTop + GlyphLibrary.Glyph.LineSpacing * Scale / 2);
        }
    }
}
