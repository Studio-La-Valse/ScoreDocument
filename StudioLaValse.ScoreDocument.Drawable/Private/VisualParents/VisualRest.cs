using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Drawable.Private.VisualParents
{
    internal sealed class VisualRest : BaseVisualNote
    {
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

                if (measureElement.Grace)
                {
                    glyph.Scale = Scale;
                }

                return new DrawableScoreGlyph(
                    XPosition,
                    HeightOnCanvas,
                    glyph,
                    HorizontalTextOrigin.Center,
                    VerticalTextOrigin.Center,
                    DisplayColor);
            }
        }

        public override bool OffsetDots => false;
        public override double XOffset => 0;

        public VisualRest(IChordReader note, double canvasLeft, double canvasTop, double lineSpacing, double scoreScale, double instrumentScale, ColorARGB color, ISelection<IUniqueScoreElement> selection) :
            base(note, canvasLeft, canvasTop, lineSpacing, scoreScale, instrumentScale, 1, color, selection)
        {

        }
    }
}
