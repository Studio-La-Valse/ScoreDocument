using StudioLaValse.Drawable.Text;

namespace StudioLaValse.ScoreDocument.Drawable.Private.Models
{
    internal sealed class Glyph
    {
        //assume: 72em = 1 inch tall (windows spec)
        //assume: 96dpi, 1 inch is 25.4mm, so 1px = 25.4/96 = 0.26458333mm (also windows spec)
        //assume: 72em = 96 pixels
        //assume: 12em = 12/72 logical inch = 1/6 logical inch = 96/6 pixels = 16 pixels = 4,23333328mm
        //assume: according to smufl spec, one font is one staff height, so one line space = 4,23333328 / 4
        //so: LineSpacing = 96 * (6 / 72d) * (25.4 / 96) / 4;
        //so: LineSpacing = 1 * (6 / 72d) * (25.4 / 1) / 4;
        //so: LineSpacing = 6 / 72d * 25.4 / 4;
        //so: I guess this works but I'm still not really sure why.
        public static readonly double Em = 12;
        public static double LineSpacingMm = Em / 72d * 25.4 / 4;


        private readonly double? knownWidth;


        public FontFamilyCore FontFamily { get; } = new FontFamilyCore(new Uri("avares://Sinfonia/Resources/"), "#Bravura");
        public double Scale { get; set; }


        public double Points => 
            Em * Scale;
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