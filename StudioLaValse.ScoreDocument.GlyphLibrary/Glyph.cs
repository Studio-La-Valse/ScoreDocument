namespace StudioLaValse.ScoreDocument.GlyphLibrary
{
    /// <summary>
    /// A sumfl-compliant font glyph.
    /// </summary>
    public class Glyph
    {
        //according to smufl spec, one font is one staff height
        //so: one line space is one fourth of the font size
        /// <summary>
        /// The font size. Defaults to 6, cannot be changed.
        /// </summary>
        public static readonly double Em = 6;
        /// <summary>
        /// The number of staff spaces.
        /// </summary>
        public static readonly int StaffSpaces = 4;
        /// <summary>
        /// The required space between staff lines to exactly fit this font.
        /// </summary>
        public static readonly double LineSpacing = Em * 1 / StaffSpaces;


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