namespace StudioLaValse.ScoreDocument.Drawable.Private.VisualParents
{
    internal abstract class BaseVisualNote : BaseSelectableParent<IUniqueScoreElement>
    {
        protected readonly IPositionElement measureElement;
        private readonly double lineSpacing;
        private readonly double scoreScale;
        private readonly double instrumentScale;
        private readonly double noteScale;
        private readonly IScoreDocumentLayout scoreDocumentLayout;
        private readonly IUnitToPixelConverter unitToPixelConverter;

        public abstract DrawableScoreGlyph Glyph { get; }
        public abstract bool OffsetDots { get; }


        public double Scale => scoreScale * instrumentScale * noteScale;
        public double XPosition => XOffset + CanvasLeft;
        public double CanvasLeft { get; }
        public abstract double XOffset { get; }
        public double HeightOnCanvas { get; }



        public RythmicDuration DisplayDuration =>
            measureElement.RythmicDuration;
        public IEnumerable<DrawableCircle> Dots
        {
            get
            {
                var lineSpacingOnStaff = unitToPixelConverter.UnitsToPixels(lineSpacing * scoreScale * instrumentScale);
                var heightOnCanvas = HeightOnCanvas;
                if (OffsetDots)
                {
                    heightOnCanvas -= lineSpacingOnStaff / 2;
                }

                var spacing = 3.5 * (GlyphLibrary.Glyph.Em / 12) * scoreScale * instrumentScale;
                var startLeft = XPosition + spacing;

                for (var i = 0; i < measureElement.RythmicDuration.Dots; i++)
                {
                    DrawableCircle circle = new(
                        new XY(startLeft, heightOnCanvas),
                        lineSpacingOnStaff / 6,
                        scoreDocumentLayout.PageForegroundColor.FromPrimitive());

                    yield return circle;

                    startLeft += spacing;
                }
            }
        }




        public BaseVisualNote(INote measureElement,
                              double canvasLeft,
                              double canvasTop,
                              double lineSpacing,
                              double scoreScale,
                              double instrumentScale,
                              double noteScale,
                              IScoreDocumentLayout scoreDocumentLayout,
                              ISelection<IUniqueScoreElement> selection,
                              IUnitToPixelConverter unitToPixelConverter) : base(measureElement, selection)
        {
            this.measureElement = measureElement;
            this.lineSpacing = lineSpacing;
            this.scoreScale = scoreScale;
            this.instrumentScale = instrumentScale;
            this.noteScale = noteScale;
            this.scoreDocumentLayout = scoreDocumentLayout;
            this.unitToPixelConverter = unitToPixelConverter;
            CanvasLeft = canvasLeft;
            HeightOnCanvas = canvasTop;
        }
        public BaseVisualNote(IChord measureElement,
                              double canvasLeft,
                              double canvasTop,
                              double lineSpacing,
                              double scoreScale,
                              double instrumentScale,
                              double noteScale,
                              IScoreDocumentLayout scoreDocumentLayout,
                              ISelection<IUniqueScoreElement> selection,
                              IUnitToPixelConverter unitToPixelConverter) : base(measureElement, selection)
        {
            this.measureElement = measureElement;
            this.lineSpacing = lineSpacing;
            this.scoreScale = scoreScale;
            this.instrumentScale = instrumentScale;
            this.noteScale = noteScale;
            this.scoreDocumentLayout = scoreDocumentLayout;
            this.unitToPixelConverter = unitToPixelConverter;
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
            return new BoundingBox(
                XPosition - unitToPixelConverter.UnitsToPixels(lineSpacing * Scale / 2), 
                XPosition + unitToPixelConverter.UnitsToPixels(lineSpacing * Scale / 2), 
                HeightOnCanvas - unitToPixelConverter.UnitsToPixels(lineSpacing * Scale / 2), 
                HeightOnCanvas + unitToPixelConverter.UnitsToPixels(lineSpacing * Scale / 2));
        }
    }
}
