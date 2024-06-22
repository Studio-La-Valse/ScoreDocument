namespace StudioLaValse.ScoreDocument.GlyphLibrary
{
    public interface IGlyphLibrary
    {
        Glyph NoteHeadBlack(double scale);
        Glyph NoteHeadWhite(double scale);
        Glyph NoteHeadWhole(double scale);
        Glyph DoubleSharp(double scale);
        Glyph Sharp(double scale);
        Glyph Natural(double scale);
        Glyph Flat(double scale);
        Glyph DoubleFlat(double scale);
        Glyph ClefG(double scale);
        Glyph ClefF(double scale);
        Glyph ClefC(double scale);
        Glyph ClefPercussion(double scale);
        Glyph Brace(double scale);
        Glyph RestWhole(double scale);
        Glyph RestHalf(double scale);
        Glyph RestQuarter(double scale);
        Glyph RestEighth(double scale);
        Glyph RestSixteenth(double scale);
        Glyph RestThirtySecond(double scale);
        Glyph FlagEighthUp(double scale);
        Glyph FlagSixteenthUp(double scale);
        Glyph FlagThirtySecondUp(double scale);
        Glyph FlagSixtyFourthUp(double scale);
        Glyph FlagEighthDown(double scale);
        Glyph FlagSixteenthDown(double scale);
        Glyph FlagThirtySecondDown(double scale);
        Glyph FlagSixtyFourthDown(double scale);
        Glyph NumberZero(double scale);
        Glyph NumberOne(double scale);
        Glyph NumberTwo(double scale);
        Glyph NumberThree(double scale);
        Glyph NumberFour(double scale);
        Glyph NumberFive(double scale);
        Glyph NumberSix(double scale);
        Glyph NumberSeven(double scale);
        Glyph NumberEight(double scale);
        Glyph NumberNine(double scale);
        Glyph TimeSignatureCommon(double scale);
        Glyph TimeSignatureCut(double scale);
    }
}