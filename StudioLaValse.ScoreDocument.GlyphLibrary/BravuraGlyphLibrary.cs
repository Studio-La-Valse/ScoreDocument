namespace StudioLaValse.ScoreDocument.GlyphLibrary
{
    /// <summary>
    /// A glyph library for the bravura font family.
    /// </summary>
    public class BravuraGlyphLibrary : BaseGlyphLibrary
    {
        /// <inheritdoc/>
        public override Uri FontFamilyKey { get; } = new Uri("avares://Sinfonia/Resources/Fonts/bravura");

        /// <inheritdoc/>
        public override string FontFamily { get; } = "#Bravura";
    }
}