using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Private.VisualParents
{
    internal sealed class VisualRest : BaseVisualNote
    {
        private readonly IGlyphLibrary glyphLibrary;
        private readonly IScoreDocument scoreDocumentLayout;

        public Glyph GlyphPrototype
        {
            get
            {
                var duration = 1M;

                var glyphs = new[]
                {
                    glyphLibrary.RestWhole(Scale),
                    glyphLibrary.RestHalf(Scale),
                    glyphLibrary.RestQuarter(Scale),
                    glyphLibrary.RestEighth(Scale),
                    glyphLibrary.RestSixteenth(Scale),
                    glyphLibrary.RestThirtySecond(Scale),
                };

                for (var i = 0; i < 6; i++)
                {
                    if (DisplayDuration.Decimal >= duration)
                    {
                        return glyphs[i];
                    }

                    duration /= 2;
                }

                return glyphs[3];
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

        public VisualRest(IChord note,
                          double canvasLeft,
                          double canvasTop,
                          double lineSpacing,
                          double scoreScale,
                          double instrumentScale,
                          IGlyphLibrary glyphLibrary,
                          IScoreDocument scoreDocumentLayout,
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
            this.glyphLibrary = glyphLibrary;
            this.scoreDocumentLayout = scoreDocumentLayout;
        }
    }
}
