using StudioLaValse.Drawable.DrawableElements;
using StudioLaValse.Drawable.Text;
using StudioLaValse.Geometry;
using StudioLaValse.ScoreDocument.Drawable.Models;

namespace StudioLaValse.ScoreDocument.Drawable.DrawableElements
{
    public sealed class DrawableScoreGlyph : DrawableText
    {
        public DrawableScoreGlyph(double topLeftX, double topLeftY, Glyph glyph, ColorARGB color) :
            base(topLeftX, topLeftY - glyph.HeightCorrection, glyph.AsString, glyph.Points, color, HorizontalTextOrigin.Left, VerticalTextOrigin.Top, glyph.FontFamily)
        {

        }

        public DrawableScoreGlyph(double locationX, double locationY, Glyph glyph, HorizontalTextOrigin horizontalTextOrigin, VerticalTextOrigin verticalTextOrigin, ColorARGB color) :
            base(locationX, locationY, glyph.AsString, glyph.Points, color, horizontalTextOrigin, verticalTextOrigin, glyph.FontFamily)
        {

        }

        public override BoundingBox GetBoundingBox()
        {
            //.. magic performance booster would be nice
            return base.GetBoundingBox();
        }
    }
}
