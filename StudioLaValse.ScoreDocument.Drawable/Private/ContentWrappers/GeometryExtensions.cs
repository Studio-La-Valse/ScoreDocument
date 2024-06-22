using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal static class GeometryExtensions
    {
        public static ColorARGB FromPrimitive(this Layout.Templates.ColorARGB color)
        {
            return new ColorARGB(color.A, color.R, color.G, color.B);
        }

        public static XY Measure(this Glyph glyph)
        {
            if(glyph.KnownWidth is null || glyph.KnownHeight is null)
            {
                return ExternalTextMeasure.TextMeasurer.Measure(glyph.StringValue, new(glyph.FontFamilyKey!, glyph.FontFamily), glyph.Points);
            }
            else
            {
                return new XY(glyph.KnownWidth.Value * glyph.Scale, glyph.KnownHeight.Value * glyph.Scale);
            }
        }

        public static double Width(this Glyph glyph)
        {
            if (glyph.KnownWidth is null)
            {
                return ExternalTextMeasure.TextMeasurer.Measure(glyph.StringValue, new(glyph.FontFamilyKey!, glyph.FontFamily), glyph.Points).X;
            }
            else
            {
                return glyph.KnownWidth.Value * glyph.Scale;
            }
        }
    }
}
