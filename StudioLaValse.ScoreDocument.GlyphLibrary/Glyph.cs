namespace StudioLaValse.ScoreDocument.GlyphLibrary
{
    public class Glyph
    {
        //assume: 72em = 1 inch tall (windows spec)
        //assume: 96dpi, 1 inch is 25.4mm, so 1px = 25.4/96 = 0.26458333mm (also windows spec)
        //assume: 72em = 96 pixels
        //assume: 12em = 12/72 logical inch = 1/6 logical inch = 96/6 pixels = 16 pixels = 4,23333328mm
        //assume: according to smufl spec, one font is one staff height, so one line space = 4,23333328 / 4
        //so: LineSpacing = 96 * (em / 72d) * (25.4 / 96) / 4;
        //so: LineSpacing = 1 * (em / 72d) * (25.4 / 1) / 4;
        //so: LineSpacing = em / 72d * 25.4 / 4;
        //so: I guess this works but I'm still not really sure why.
        public static readonly double Em = 12;
        public static readonly double LineSpacingMm = Em / 72 * 25.4 / 4;


        public double? KnownWidth { get; }
        public double? KnownHeight { get; }
        public Uri? FontFamilyKey { get; }
        public string FontFamily { get; }
        public double Scale { get; }
        public string StringValue { get; }


        public double Points =>
            Em * Scale;



        public Glyph(string stringValue, Uri? fontFamilyKey, string fontFamily, double scale = 1, double? knownWidth = null, double? knownHeight = null)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(scale, nameof(scale));

            KnownWidth = knownWidth;
            KnownHeight = knownHeight;
            Scale = scale;
            StringValue = stringValue;
            FontFamilyKey = fontFamilyKey;
            FontFamily = fontFamily;
        }

        public Glyph(string stringValue, string fontFamily, double scale = 1, double? knownWidth = null, double? knownHeight = null) : this(stringValue, null, fontFamily, scale, knownWidth, knownHeight)
        {

        }

        public override string ToString()
        {
            return StringValue;
        }
    }
}