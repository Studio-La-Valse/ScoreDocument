namespace StudioLaValse.ScoreDocument.GlyphLibrary
{
    public abstract class BaseGlyphLibrary : IGlyphLibrary
    {
        public abstract Uri FontFamilyKey { get; }
        public abstract string FontFamily { get; }


        public Glyph NoteHeadBlack(double scale) => new("\uE0A4", FontFamilyKey, FontFamily, scale);
        public Glyph NoteHeadWhite(double scale) => new("\uE0A3", FontFamilyKey, FontFamily, scale);
        public Glyph NoteHeadWhole(double scale) => new("\uE0A2", FontFamilyKey, FontFamily, scale);
        public Glyph DoubleSharp(double scale) => new("\uE263", FontFamilyKey, FontFamily, scale);
        public Glyph Sharp(double scale) => new("\uE262", FontFamilyKey, FontFamily, scale);
        public Glyph Natural(double scale) => new("\uE261", FontFamilyKey, FontFamily, scale);
        public Glyph Flat(double scale) => new("\uE260", FontFamilyKey, FontFamily, scale);
        public Glyph DoubleFlat(double scale) => new("\uE264", FontFamilyKey, FontFamily, scale);
        public Glyph ClefG(double scale) => new($"\uE050", FontFamilyKey, FontFamily, scale);
        public Glyph ClefF(double scale) => new($"\uE062", FontFamilyKey, FontFamily, scale);
        public Glyph ClefC(double scale) => new($"\uE05B", FontFamilyKey, FontFamily, scale);
        public Glyph ClefPercussion(double scale) => new($"\uE068", FontFamilyKey, FontFamily, scale);
        public Glyph Brace(double scale) => new($"\uE000", FontFamilyKey, FontFamily, scale);
        public Glyph RestWhole(double scale) => new($"\uE4E3", FontFamilyKey, FontFamily, scale);
        public Glyph RestHalf(double scale) => new($"\uE4E4", FontFamilyKey, FontFamily, scale);
        public Glyph RestQuarter(double scale) => new($"\uE4E5", FontFamilyKey, FontFamily, scale);
        public Glyph RestEighth(double scale) => new($"\uE4E6", FontFamilyKey, FontFamily, scale);
        public Glyph RestSixteenth(double scale) => new($"\uE4E7", FontFamilyKey, FontFamily, scale);
        public Glyph RestThirtySecond(double scale) => new("\uE4E8", FontFamilyKey, FontFamily, scale);
        public Glyph FlagEighthUp(double scale) => new("\uE240", FontFamilyKey, FontFamily, scale);
        public Glyph FlagSixteenthUp(double scale) => new("\uE242", FontFamilyKey, FontFamily, scale);
        public Glyph FlagThirtySecondUp(double scale) => new("\uE244", FontFamilyKey, FontFamily, scale);
        public Glyph FlagSixtyFourthUp(double scale) => new("\uE246", FontFamilyKey, FontFamily, scale);
        public Glyph FlagEighthDown(double scale) => new("\uE241", FontFamilyKey, FontFamily, scale);
        public Glyph FlagSixteenthDown(double scale) => new("\uE243", FontFamilyKey, FontFamily, scale);
        public Glyph FlagThirtySecondDown(double scale) => new("\uE245", FontFamilyKey, FontFamily, scale);
        public Glyph FlagSixtyFourthDown(double scale) => new("\uE247", FontFamilyKey, FontFamily, scale);
        public Glyph NumberZero(double scale) => new("\uE080", FontFamilyKey, FontFamily, scale);
        public Glyph NumberOne(double scale) => new("\uE081", FontFamilyKey, FontFamily, scale);
        public Glyph NumberTwo(double scale) => new("\uE082", FontFamilyKey, FontFamily, scale);
        public Glyph NumberThree(double scale) => new("\uE083", FontFamilyKey, FontFamily, scale);
        public Glyph NumberFour(double scale) => new("\uE084", FontFamilyKey, FontFamily, scale);
        public Glyph NumberFive(double scale) => new("\uE085", FontFamilyKey, FontFamily, scale);
        public Glyph NumberSix(double scale) => new("\uE086", FontFamilyKey, FontFamily, scale);
        public Glyph NumberSeven(double scale) => new("\uE087", FontFamilyKey, FontFamily, scale);
        public Glyph NumberEight(double scale) => new("\uE088", FontFamilyKey, FontFamily, scale);
        public Glyph NumberNine(double scale) => new("\uE089", FontFamilyKey, FontFamily, scale);
        public Glyph TimeSignatureCommon(double scale) => new("\uE08A", FontFamilyKey, FontFamily, scale);
        public Glyph TimeSignatureCut(double scale) => new("\uE08B", FontFamilyKey, FontFamily, scale);
    }
}