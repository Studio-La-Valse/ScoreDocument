namespace StudioLaValse.ScoreDocument.Drawable.Private.Visuals.Models
{
    internal static class GlyphLibrary
    {
        public static Glyph NoteHeadBlack => new Glyph("\uE0A4", knownWidth: 1.34);
        public static Glyph NoteHeadWhite => new Glyph("\uE0A3", knownWidth: 1.29);
        public static Glyph NoteHeadWhole => new Glyph("\uE0A2", knownWidth: 1.29);
        public static Glyph DoubleSharp => new Glyph("\uE263");
        public static Glyph Sharp => new Glyph("\uE262");
        public static Glyph Natural => new Glyph("\uE261");
        public static Glyph Flat => new Glyph("\uE260");
        public static Glyph DoubleFlat => new Glyph("\uE264");
        public static Glyph ClefG => new Glyph($"\uE050");
        public static Glyph ClefF => new Glyph($"\uE062");
        public static Glyph ClefC => new Glyph($"\uE05B");
        public static Glyph ClefPercussion => new Glyph($"\uE068");
        public static Glyph Brace => new Glyph($"\uE000");
        public static Glyph RestWhole => new Glyph($"\uE4E3");
        public static Glyph RestHalf => new Glyph($"\uE4E4");
        public static Glyph RestQuarter => new Glyph($"\uE4E5");
        public static Glyph RestEighth => new Glyph($"\uE4E6");
        public static Glyph RestSixteenth => new Glyph($"\uE4E7");
        public static Glyph RestThirtySecond => new Glyph("\uE4E8");
        public static Glyph FlagEighthUp => new Glyph("\uE240");
        public static Glyph FlagSixteenthUp => new Glyph("\uE242");
        public static Glyph FlagThirtySecondUp => new Glyph("\uE244");
        public static Glyph FlagSixtyFourthUp => new Glyph("\uE246");
        public static Glyph FlagEighthDown => new Glyph("\uE241");
        public static Glyph FlagSixteenthDown => new Glyph("\uE243");
        public static Glyph FlagThirtySecondDown => new Glyph("\uE245");
        public static Glyph FlagSixtyFourthDown => new Glyph("\uE247");
        public static Glyph NumberZero => new Glyph("\uE080");
        public static Glyph NumberOne => new Glyph("\uE081");
        public static Glyph NumberTwo => new Glyph("\uE082");
        public static Glyph NumberThree => new Glyph("\uE083");
        public static Glyph NumberFour => new Glyph("\uE084");
        public static Glyph NumberFive => new Glyph("\uE085");
        public static Glyph NumberSix => new Glyph("\uE086");
        public static Glyph NumberSeven => new Glyph("\uE087");
        public static Glyph NumberEight => new Glyph("\uE088");
        public static Glyph NumberNine => new Glyph("\uE089");
        public static Glyph TimeSignatureCommon => new Glyph("\uE08A");
        public static Glyph TimeSignatureCut => new Glyph("\uE08B");

    }
}
