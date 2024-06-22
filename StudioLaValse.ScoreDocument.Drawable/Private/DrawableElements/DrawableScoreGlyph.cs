using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Private.DrawableElements
{
    internal sealed class DrawableScoreGlyph : DrawableText
    {
        public DrawableScoreGlyph(double locationX, double locationY, Glyph glyph, HorizontalTextOrigin horizontalTextOrigin, VerticalTextOrigin verticalTextOrigin, ColorARGB color) :
            base(locationX, locationY, glyph.StringValue, glyph.Points, color, horizontalTextOrigin, verticalTextOrigin, new FontFamilyCore(glyph.FontFamilyKey!, glyph.FontFamily))
        {

        }

        public override BoundingBox GetBoundingBox()
        {
            //.. magic performance booster would be nice
            return base.GetBoundingBox();
        }
    }
}
