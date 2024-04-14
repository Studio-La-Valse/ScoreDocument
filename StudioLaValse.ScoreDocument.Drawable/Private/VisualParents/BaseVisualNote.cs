namespace StudioLaValse.ScoreDocument.Drawable.Private.VisualParents
{
    internal abstract class BaseVisualNote : BaseSelectableParent<IUniqueScoreElement>
    {
        protected readonly IPositionElement measureElement;
        private readonly double lineSpacing;
        private readonly double scoreScale;
        private readonly double instrumentScale;
        private readonly double noteScale;
        private readonly ISelection<IUniqueScoreElement> selection;

        public abstract DrawableScoreGlyph Glyph { get; }
        public abstract bool OffsetDots { get; }


        public double Scale => scoreScale * instrumentScale * noteScale;
        public double XPosition => XOffset + CanvasLeft;
        public double CanvasLeft { get; }
        public abstract double XOffset { get; }
        public double HeightOnCanvas { get; }



        public RythmicDuration DisplayDuration =>
            measureElement.RythmicDuration;
        public ColorARGB DisplayColor { get; }
        public IEnumerable<DrawableCircle> Dots
        {
            get
            {
                var lineSpacingOnStaff = lineSpacing * scoreScale * instrumentScale;
                var heightOnCanvas = HeightOnCanvas;
                if (OffsetDots)
                {
                    heightOnCanvas -= lineSpacingOnStaff / 2;
                }

                var spacing = 1.5 * Scale;
                var startLeft = XPosition + spacing;

                for (var i = 0; i < measureElement.RythmicDuration.Dots; i++)
                {
                    DrawableCircle circle = new(
                        new XY(startLeft, heightOnCanvas),
                        0.3 * Scale,
                        DisplayColor);

                    yield return circle;

                    startLeft += spacing;
                }
            }
        }




        public BaseVisualNote(INoteReader measureElement, double canvasLeft, double canvasTop, double lineSpacing, double scoreScale, double instrumentScale, double noteScale, ColorARGB defaultColor, ISelection<IUniqueScoreElement> selection) : base(measureElement, selection)
        {
            this.measureElement = measureElement;
            this.selection = selection;

            this.lineSpacing = lineSpacing;
            this.scoreScale = scoreScale;
            this.instrumentScale = instrumentScale;
            this.noteScale = noteScale;

            DisplayColor = defaultColor;
            CanvasLeft = canvasLeft;
            HeightOnCanvas = canvasTop;
        }
        public BaseVisualNote(IChordReader measureElement, double canvasLeft, double canvasTop, double lineSpacing, double scoreScale, double instrumentScale, double noteScale, ColorARGB defaultColor, ISelection<IUniqueScoreElement> selection) : base(measureElement, selection)
        {
            this.measureElement = measureElement;
            this.selection = selection;

            this.lineSpacing = lineSpacing;
            this.scoreScale = scoreScale;
            this.instrumentScale = instrumentScale;
            this.noteScale = noteScale;

            DisplayColor = defaultColor;
            CanvasLeft = canvasLeft;
            HeightOnCanvas = canvasTop;
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
            yield return new SimpleGhost(this);
        }
        public override bool Respond(XY point)
        {
            return BoundingBox().Contains(point);
        }
        public override BoundingBox BoundingBox()
        {
            return new BoundingBox(XPosition - (lineSpacing * Scale / 2), XPosition + (lineSpacing * Scale / 2), HeightOnCanvas - (lineSpacing * Scale / 2), HeightOnCanvas + (lineSpacing * Scale / 2));
        }
    }
}
