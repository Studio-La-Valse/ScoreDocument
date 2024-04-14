namespace StudioLaValse.ScoreDocument.Drawable.Private.DrawableElements
{
    internal sealed class DrawableScoreGlyph : DrawableText
    {
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
