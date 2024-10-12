using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    /// <summary>
    /// Some geometry extensions.
    /// </summary>
    public static class GeometryExtensions
    {
        /// <summary>
        /// Create a color from a simple color.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static ColorARGB FromPrimitive(this StyleTemplates.ColorARGB color)
        {
            return new ColorARGB(color.A, color.R, color.G, color.B);
        }

        /// <summary>
        /// Measure the size of the glyph.
        /// </summary>
        /// <param name="glyph"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Measure the width of the glyph.
        /// </summary>
        /// <param name="glyph"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Measure the height of the glyph.
        /// </summary>
        /// <param name="glyph"></param>
        /// <returns></returns>
        public static double Height(this Glyph glyph)
        {
            if (glyph.KnownHeight is null)
            {
                return ExternalTextMeasure.TextMeasurer.Measure(glyph.StringValue, new(glyph.FontFamilyKey!, glyph.FontFamily), glyph.Points).Y;
            }
            else
            {
                return glyph.KnownHeight.Value * glyph.Scale;
            }
        }
    }
}
