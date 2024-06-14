using StudioLaValse.Drawable.Text;

namespace StudioLaValse.ScoreDocument.Drawable.Private.Models
{
    internal sealed class Glyph
    {
        //assume: 72em = 1 inch tall (windows spec)
        //assume: 96dpi, 1 inch is 25.4mm, so 1px = 25.4/96 = 0.26458333mm (also windows spec)
        //assume: 72em = 96 pixels
        //assume: 6em = 6/72 logical inch = 1/12 logical inch = 96/12 pixels = 8 pixels = 2,11666664mm
        //assume: according to smufl spec, one font is one staff height, so one line space = 2,116666 / 4
        //so: LineSpacing = 96 * (6 / 72d) * (25.4 / 96) / 4;
        //so: LineSpacing = 1 * (6 / 72d) * (25.4 / 1) / 4;
        //so: LineSpacing = 6 / 72d * 25.4 / 4;
        //so: I guess this works but I'm still not really sure why.
        private static readonly double points = 6;
        public static double LineSpacing = points / 72d * 25.4 / 4;


        private readonly double? knownWidth;


        public FontFamilyCore FontFamily { get; } = new FontFamilyCore(new Uri("avares://Sinfonia/Resources/"), "#Bravura");
        public double Scale { get; set; }


        public double Points => 
            points * Scale;
        public double Width =>
            knownWidth.HasValue ?
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