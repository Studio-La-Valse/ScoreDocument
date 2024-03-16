namespace StudioLaValse.ScoreDocument.Drawable.Private.VisualParents
{
    internal abstract class BaseVisualNote : BaseSelectableParent<IUniqueScoreElement>
    {
        protected readonly IPositionElement measureElement;

        public abstract DrawableScoreGlyph Glyph { get; }
        public abstract bool OffsetDots { get; }


        public double Scale { get; }
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
                double heightOnCanvas = HeightOnCanvas;
                if (OffsetDots)
                {
                    heightOnCanvas -= 0.6;
                }

                double spacing = 1.3;
                double startLeft = XPosition + spacing;

                for (int i = 0; i < measureElement.RythmicDuration.Dots; i++)
                {
                    DrawableCircle circle = new(
                        new XY(startLeft, heightOnCanvas),
                        0.3,
                        DisplayColor);

                    yield return circle;

                    startLeft += spacing;
                }
            }
        }




        public BaseVisualNote(INoteReader measureElement, double canvasLeft, double canvasTop, double scale, ColorARGB defaultColor, ISelection<IUniqueScoreElement> selection) : base(measureElement, selection)
        {
            this.measureElement = measureElement;
            DisplayColor = defaultColor ?? new ColorARGB(255, 0, 0, 0);

            CanvasLeft = canvasLeft;
            HeightOnCanvas = canvasTop;
            Scale = scale;
        }
        public BaseVisualNote(IChordReader measureElement, double canvasLeft, double canvasTop, double scale, ColorARGB defaultColor, ISelection<IUniqueScoreElement> selection) : base(measureElement, selection)
        {
            this.measureElement = measureElement;
            DisplayColor = defaultColor ?? new ColorARGB(255, 0, 0, 0);

            CanvasLeft = canvasLeft;
            HeightOnCanvas = canvasTop;
            Scale = scale;
        }


        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            yield return Glyph;

            foreach (DrawableCircle dot in Dots)
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
            return new BoundingBox(XPosition - 0.5, XPosition + 0.5, HeightOnCanvas - 0.5, HeightOnCanvas + 0.5);
        }
    }
}
