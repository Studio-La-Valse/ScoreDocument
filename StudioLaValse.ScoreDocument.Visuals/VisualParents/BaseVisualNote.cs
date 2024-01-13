﻿using StudioLaValse.Drawable.ContentWrappers;
using StudioLaValse.Drawable.DrawableElements;
using StudioLaValse.Drawable.Interaction.ContentWrappers;
using StudioLaValse.Drawable.Interaction.Selection;
using StudioLaValse.Geometry;
using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Drawable.DrawableElements;
using StudioLaValse.ScoreDocument.Drawable.Ghosts;
using StudioLaValse.ScoreDocument.Extensions;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Drawable.VisualParents
{
    public abstract class BaseVisualNote : BaseSelectableParent<IUniqueScoreElement>
    {
        protected readonly IPositionElement measureElement;
        private readonly ColorARGB defaultColor;


        public abstract DrawableScoreGlyph Glyph { get; }
        public abstract int Line { get; }


        public double Scale { get; }
        public double XPosition { get; }
        public double HeightOnCanvas { get; }



        public XY CenterPosition =>
            new XY(XPosition, HeightOnCanvas);
        public Fraction ActualDuration =>
            measureElement.ActualDuration();
        public RythmicDuration DisplayDuration =>
            measureElement.RythmicDuration;
        public ColorARGB DisplayColor =>
            defaultColor;
        public IEnumerable<DrawableCircle> Dots
        {
            get
            {
                var dots = new List<DrawableCircle>();

                var heightOnCanvas = HeightOnCanvas;
                if (MathUtils.UnsignedModulo(Line, 2) == 0)
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

                    dots.Add(circle);

                    startLeft += spacing;
                }

                return dots;
            }
        }




        public BaseVisualNote(INote measureElement, double canvasLeft, double canvasTop, double scale, ColorARGB defaultColor, ISelection<IUniqueScoreElement> selection) : base(measureElement, selection)
        {
            this.measureElement = measureElement;
            this.defaultColor = defaultColor ?? new ColorARGB(255, 0, 0, 0);

            XPosition = canvasLeft;
            HeightOnCanvas = canvasTop;
            Scale = scale;
        }
        public BaseVisualNote(IChord measureElement, double canvasLeft, double canvasTop, double scale, ColorARGB defaultColor, ISelection<IUniqueScoreElement> selection) : base(measureElement, selection)
        {
            this.measureElement = measureElement;
            this.defaultColor = defaultColor ?? new ColorARGB(255, 0, 0, 0);

            XPosition = canvasLeft;
            HeightOnCanvas = canvasTop;
            Scale = scale;
        }


        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            var elements = new List<BaseDrawableElement>
            {
                Glyph,
            };

            elements.AddRange(Dots);

            return elements;
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            return new List<BaseContentWrapper>()
            {
                new SimpleGhost(this)
            };
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
