using StudioLaValse.ScoreDocument.Core.Primitives;
using StudioLaValse.ScoreDocument.Drawable.Private.Visuals.DrawableElements;

namespace StudioLaValse.ScoreDocument.Drawable.Private.Visuals.VisualParents
{
    internal abstract class BaseVisualNote : BaseSelectableParent<IUniqueScoreElement>
    {
        protected readonly IPositionElement measureElement;
        private readonly ColorARGB defaultColor;


        public abstract DrawableScoreGlyph Glyph { get; }
        public abstract bool OffsetDots { get; }


        public double Scale { get; }
        public double XPosition => XOffset + CanvasLeft;
        public double CanvasLeft { get; }
        public abstract double XOffset { get; }
        public double HeightOnCanvas { get; }



        public RythmicDuration DisplayDuration =>
            measureElement.RythmicDuration;
        public ColorARGB DisplayColor =>
            defaultColor;
        public IEnumerable<DrawableCircle> Dots
        {
            get
            {
                var heightOnCanvas = HeightOnCanvas;
                if (OffsetDots)
                {
                    heightOnCanvas -= 0.6;
                }

                var spacing = 1.3;
                var startLeft = XPosition + spacing;

                for (int i = 0; i < measureElement.RythmicDuration.Dots; i++)
                {
                    var circle = new DrawableCircle(
                        new XY(startLeft, heightOnCanvas),
                        0.3,
                        defaultColor);

                    yield return circle;

                    startLeft += spacing;
                }
            }
        }




        public BaseVisualNote(INote measureElement, double canvasLeft, double canvasTop, double scale, ColorARGB defaultColor, ISelection<IUniqueScoreElement> selection) : base(measureElement, selection)
        {
            this.measureElement = measureElement;
            this.defaultColor = defaultColor ?? new ColorARGB(255, 0, 0, 0);

            CanvasLeft = canvasLeft;
            HeightOnCanvas = canvasTop;
            Scale = scale;
        }
        public BaseVisualNote(IChord measureElement, double canvasLeft, double canvasTop, double scale, ColorARGB defaultColor, ISelection<IUniqueScoreElement> selection) : base(measureElement, selection)
        {
            this.measureElement = measureElement;
            this.defaultColor = defaultColor ?? new ColorARGB(255, 0, 0, 0);

            CanvasLeft = canvasLeft;
            HeightOnCanvas = canvasTop;
            Scale = scale;
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
            return new BoundingBox(XPosition - 0.5, XPosition + 0.5, HeightOnCanvas - 0.5, HeightOnCanvas + 0.5);
        }
    }
}
