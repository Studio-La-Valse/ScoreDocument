namespace StudioLaValse.ScoreDocument.GlyphLibrary
{
    /// <summary>
    /// A sumfl-compliant font glyph.
    /// </summary>
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
        /// <summary>
        /// The font size. Defaults to 12, cannot be changed.
        /// </summary>
        public static readonly double Em = 12;
        /// <summary>
        /// Pixels per inch.
        /// </summary>
        public static readonly double PixelsPerInch = 72;
        /// <summary>
        /// Millimeters per inch.
        /// </summary>
        public static readonly double MmPerInch = 25.4;
        /// <summary>
        /// The number of staff spaces.
        /// </summary>
        public static readonly int StaffSpaces = 4;
        /// <summary>
        /// The required space between staff lines to exactly fit this font.
        /// </summary>
        public static readonly double LineSpacingMm = Em / PixelsPerInch * MmPerInch / StaffSpaces;


        /// <summary>
        /// If known, the height of this glyph at scale 1.
        /// </summary>
        public double? KnownWidth { get; }
        /// <summary>
        /// If known, the width of this glyph at scale 1.
        /// </summary>
        public double? KnownHeight { get; }
        /// <summary>
        /// The font family key of this glyph's font family.
        /// </summary>
        public Uri? FontFamilyKey { get; }
        /// <summary>
        /// The font family key.
        /// </summary>
        public string FontFamily { get; }
        /// <summary>
        /// The scale at which to draw this glyph.
        /// </summary>
        public double Scale { get; }
        /// <summary>
        /// The string value of the glyph.
        /// </summary>
        public string StringValue { get; }

        /// <summary>
        /// The number of points after scaling.
        /// </summary>
        public double Points =>
            Em * Scale;


        /// <summary>
        /// Constructs a glyph.
        /// </summary>
        /// <param name="stringValue"></param>
        /// <param name="fontFamilyKey"></param>
        /// <param name="fontFamily"></param>
        /// <param name="scale"></param>
        /// <param name="knownWidth"></param>
        /// <param name="knownHeight"></param>
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

        /// <summary>
        /// Constructs a glyph.
        /// </summary>
        /// <param name="stringValue"></param>
        /// <param name="fontFamily"></param>
        /// <param name="scale"></param>
        /// <param name="knownWidth"></param>
        /// <param name="knownHeight"></param>
        public Glyph(string stringValue, string fontFamily, double scale = 1, double? knownWidth = null, double? knownHeight = null) : this(stringValue, null, fontFamily, scale, knownWidth, knownHeight)
        {

        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return StringValue;
        }
    }
}