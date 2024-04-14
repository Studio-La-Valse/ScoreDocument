namespace StudioLaValse.ScoreDocument.Drawable.Private.Models
{
    internal sealed class Glyph
    {
        public FontFamilyCore FontFamily { get; } = new FontFamilyCore(new Uri("pack://application:,,,/Resources/"), "./#Bravura");

        private readonly double points = 6;
        private readonly double? knownWidth;

        public double Points => points * Scale;

        public double Scale { get; set; }

        public double Width => knownWidth.HasValue ?
            knownWidth.Value * Scale :
            new DrawableText(0, 0, AsString, Points, ColorARGB.White, font: FontFamily).Dimensions.X;

        public string AsString { get; }

        internal Glyph(string asString, double scale = 1, double? knownWidth = null)
        {
            this.knownWidth = knownWidth;

            Scale = scale;

            AsString = asString;
        }

        public override string ToString()
        {
            return AsString;
        }
    }
}
