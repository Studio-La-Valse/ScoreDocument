using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Private.DrawableElements
{
    internal sealed class DrawableScoreGlyph : DrawableText
    {
        private readonly Glyph glyph;

        public DrawableScoreGlyph(double locationX, double locationY, Glyph glyph, HorizontalTextOrigin horizontalTextOrigin, VerticalTextOrigin verticalTextOrigin, ColorARGB color) :
            base(locationX, locationY, glyph.StringValue, glyph.Points, color, horizontalTextOrigin, verticalTextOrigin, new FontFamilyCore(glyph.FontFamilyKey!, glyph.FontFamily))
        {
            this.glyph = glyph;
        }


        private double GetLeft(double knownWidth)
        {
            var left = OriginX;
            if (HorizontalAlignment == HorizontalTextOrigin.Left)
            {
                return left;
            }

            if (HorizontalAlignment == HorizontalTextOrigin.Center)
            {
                left -= knownWidth / 2;
                return left;
            }

            if (HorizontalAlignment == HorizontalTextOrigin.Right)
            {
                left -= knownWidth;
                return left;
            }

            throw new NotImplementedException(nameof(HorizontalAlignment));
        }

        private double GetTop(double knownHeight)
        {
            var top = OriginY;
            if (VerticalAlignment == VerticalTextOrigin.Top)
            {
                return top;
            }

            if (VerticalAlignment == VerticalTextOrigin.Center)
            {
                top -= knownHeight / 2;
                return top;
            }

            if (VerticalAlignment == VerticalTextOrigin.Bottom)
            {
                top -= knownHeight;
                return top;
            }

            throw new NotImplementedException(nameof(HorizontalAlignment));
        }

        public override BoundingBox GetBoundingBox()
        {
            if(!glyph.KnownWidth.HasValue || !glyph.KnownHeight.HasValue)
            {
                return base.GetBoundingBox();
            }

            var left = GetLeft(glyph.KnownWidth.Value);
            var top = GetTop(glyph.KnownHeight.Value);

            return new BoundingBox(left, left + glyph.KnownWidth.Value, top, top + glyph.KnownHeight.Value);
        }
    }
}
