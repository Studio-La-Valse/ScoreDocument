namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal static class GeometryExtensions
    {
        public static ColorARGB FromPrimitive(this Layout.Templates.ColorARGB color)
        {
            return new ColorARGB(color.A, color.R, color.G, color.B);
        }
    }
}
