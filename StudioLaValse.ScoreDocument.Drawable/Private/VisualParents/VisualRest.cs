using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Drawable.Private.VisualParents
{
    internal sealed class VisualRest : BaseVisualNote
    {
        private readonly IScoreDocumentLayout scoreDocumentLayout;

        public Glyph GlyphPrototype
        {
            get
            {
                var duration = 1M;

                var glyphs = new[]
                {
                    GlyphLibrary.RestWhole,
                    GlyphLibrary.RestHalf,
                    GlyphLibrary.RestQuarter,
                    GlyphLibrary.RestEighth,
                    GlyphLibrary.RestSixteenth,
                    GlyphLibrary.RestThirtySecond,
                };

                for (var i = 0; i < 6; i++)
                {
                    if (DisplayDuration.Decimal >= duration)
                    {
                        return glyphs[i];
                    }

                    duration /= 2;
                }

                return GlyphLibrary.RestEighth;
            }
        }
        public override DrawableScoreGlyph Glyph
        {
            get
            {
                var glyph = GlyphPrototype;

                return new DrawableScoreGlyph(
                    XPosition,
                    HeightOnCanvas,
                    glyph,
                    HorizontalTextOrigin.Center,
                    VerticalTextOrigin.Center,
                    scoreDocumentLayout.PageForegroundColor.FromPrimitive());
            }
        }

        public override bool OffsetDots => false;
        public override double XOffset => 0;

        public VisualRest(IChordReader note,
                          double canvasLeft,
                          double canvasTop,
                          double lineSpacing,
                          double scoreScale,
                          double instrumentScale,
                          IScoreDocumentLayout scoreDocumentLayout,
                          ISelection<IUniqueScoreElement> selection,
                          IUnitToPixelConverter unitToPixelConverter) :
            base(note,
                 canvasLeft,
                 canvasTop,
                 lineSpacing,
                 scoreScale,
                 instrumentScale,
                 1,
                 scoreDocumentLayout,
                 selection,
                 unitToPixelConverter)
        {
            this.scoreDocumentLayout = scoreDocumentLayout;
        }
    }
}
