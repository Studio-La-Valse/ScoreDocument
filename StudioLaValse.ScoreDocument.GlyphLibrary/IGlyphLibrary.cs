namespace StudioLaValse.ScoreDocument.GlyphLibrary
{
    /// <summary>
    /// An interface for a glyph library.
    /// </summary>
    public interface IGlyphLibrary
    {
        /// <summary>
        /// A default black note head.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph NoteHeadBlack(double scale);
        /// <summary>
        /// A default white note head.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph NoteHeadWhite(double scale);
        /// <summary>
        /// A default whole note head.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph NoteHeadWhole(double scale);
        /// <summary>
        /// A double sharp.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph DoubleSharp(double scale);
        /// <summary>
        /// A single sharp.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph Sharp(double scale);
        /// <summary>
        /// A single natural.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph Natural(double scale);
        /// <summary>
        /// A single flat.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph Flat(double scale);
        /// <summary>
        /// A double flat.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph DoubleFlat(double scale);
        /// <summary>
        /// A G-type clef.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph ClefG(double scale);
        /// <summary>
        /// An F-type clef.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph ClefF(double scale);
        /// <summary>
        /// A C-type clef.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph ClefC(double scale);
        /// <summary>
        /// A Percussion-type clef.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph ClefPercussion(double scale);
        /// <summary>
        /// A curly brace.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph Brace(double scale);
        /// <summary>
        /// A whole rest.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph RestWhole(double scale);
        /// <summary>
        /// A half duration rest.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph RestHalf(double scale);
        /// <summary>
        /// A quarter rest.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph RestQuarter(double scale);
        /// <summary>
        /// An eighth rest.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph RestEighth(double scale);
        /// <summary>
        /// A sixteenth rest.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph RestSixteenth(double scale);
        /// <summary>
        /// A thirty second rest.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph RestThirtySecond(double scale);
        /// <summary>
        /// An eighth flag up.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph FlagEighthUp(double scale);
        /// <summary>
        /// A sixteenth flag up.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph FlagSixteenthUp(double scale);
        /// <summary>
        /// A thirty second flag up.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph FlagThirtySecondUp(double scale);
        /// <summary>
        /// A sixty fourth flag up.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph FlagSixtyFourthUp(double scale);
        /// <summary>
        /// An eighth flag down.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph FlagEighthDown(double scale);
        /// <summary>
        /// A sixteenth flag down.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph FlagSixteenthDown(double scale);
        /// <summary>
        /// A thirty second flag down.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph FlagThirtySecondDown(double scale);
        /// <summary>
        /// A sixty fourth flag down.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph FlagSixtyFourthDown(double scale);
        /// <summary>
        /// A number 0.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph NumberZero(double scale);
        /// <summary>
        /// A number 1.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph NumberOne(double scale);
        /// <summary>
        /// A number 2.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph NumberTwo(double scale);
        /// <summary>
        /// A number 3.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph NumberThree(double scale);
        /// <summary>
        /// A number 4.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph NumberFour(double scale);
        /// <summary>
        /// A number 5.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph NumberFive(double scale);
        /// <summary>
        /// A number 6.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph NumberSix(double scale);
        /// <summary>
        /// A number 7.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph NumberSeven(double scale);
        /// <summary>
        /// A number 8.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph NumberEight(double scale);
        /// <summary>
        /// A number 9.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph NumberNine(double scale);
        /// <summary>
        /// A common time signature.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph TimeSignatureCommon(double scale);
        /// <summary>
        /// A cut time signature.
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        Glyph TimeSignatureCut(double scale);
    }
}