namespace StudioLaValse.ScoreDocument.GlyphLibrary
{
    /// <summary>
    /// An abstract class for a glyph library using a single font family.
    /// </summary>
    public abstract class BaseGlyphLibrary : IGlyphLibrary
    {
        /// <summary>
        /// The font family key.
        /// </summary>
        public abstract Uri FontFamilyKey { get; }
        /// <summary>
        /// The font family name.
        /// </summary>
        public abstract string FontFamily { get; }

        /// <inheritdoc/>
        public Glyph NoteHeadBlack(double scale) => new("\uE0A4", FontFamilyKey, FontFamily, scale);
        
        /// <inheritdoc/>
        public Glyph NoteHeadWhite(double scale) => new("\uE0A3", FontFamilyKey, FontFamily, scale);
        
        /// <inheritdoc/>
        public Glyph NoteHeadWhole(double scale) => new("\uE0A2", FontFamilyKey, FontFamily, scale);
        
        /// <inheritdoc/>
        public Glyph DoubleSharp(double scale) => new("\uE263", FontFamilyKey, FontFamily, scale);
       
        /// <inheritdoc/>
        public Glyph Sharp(double scale) => new("\uE262", FontFamilyKey, FontFamily, scale);
       
        /// <inheritdoc/>
        public Glyph Natural(double scale) => new("\uE261", FontFamilyKey, FontFamily, scale);
     
        /// <inheritdoc/>
        public Glyph Flat(double scale) => new("\uE260", FontFamilyKey, FontFamily, scale);
    
        /// <inheritdoc/>
        public Glyph DoubleFlat(double scale) => new("\uE264", FontFamilyKey, FontFamily, scale);
   
        /// <inheritdoc/>
        public Glyph ClefG(double scale) => new($"\uE050", FontFamilyKey, FontFamily, scale);
     
        /// <inheritdoc/>
        public Glyph ClefF(double scale) => new($"\uE062", FontFamilyKey, FontFamily, scale);
  
        /// <inheritdoc/>
        public Glyph ClefC(double scale) => new($"\uE05B", FontFamilyKey, FontFamily, scale);
  
        /// <inheritdoc/>
        public Glyph ClefPercussion(double scale) => new($"\uE068", FontFamilyKey, FontFamily, scale);
    
        /// <inheritdoc/>
        public Glyph Brace(double scale) => new($"\uE000", FontFamilyKey, FontFamily, scale);
    
        /// <inheritdoc/>
        public Glyph RestWhole(double scale) => new($"\uE4E3", FontFamilyKey, FontFamily, scale);
     
        /// <inheritdoc/>
        public Glyph RestHalf(double scale) => new($"\uE4E4", FontFamilyKey, FontFamily, scale);
    
        /// <inheritdoc/>
        public Glyph RestQuarter(double scale) => new($"\uE4E5", FontFamilyKey, FontFamily, scale);
    
        /// <inheritdoc/>
    
        public Glyph RestEighth(double scale) => new($"\uE4E6", FontFamilyKey, FontFamily, scale);
    
        /// <inheritdoc/>
        public Glyph RestSixteenth(double scale) => new($"\uE4E7", FontFamilyKey, FontFamily, scale);
   
        /// <inheritdoc/>
        public Glyph RestThirtySecond(double scale) => new("\uE4E8", FontFamilyKey, FontFamily, scale);
     
        /// <inheritdoc/>
        public Glyph FlagEighthUp(double scale) => new("\uE240", FontFamilyKey, FontFamily, scale);
       
        /// <inheritdoc/>
        public Glyph FlagSixteenthUp(double scale) => new("\uE242", FontFamilyKey, FontFamily, scale);
       
        /// <inheritdoc/>
        public Glyph FlagThirtySecondUp(double scale) => new("\uE244", FontFamilyKey, FontFamily, scale);
     
        /// <inheritdoc/>
        public Glyph FlagSixtyFourthUp(double scale) => new("\uE246", FontFamilyKey, FontFamily, scale);
      
        /// <inheritdoc/>
        public Glyph FlagEighthDown(double scale) => new("\uE241", FontFamilyKey, FontFamily, scale);
      
        /// <inheritdoc/>
        public Glyph FlagSixteenthDown(double scale) => new("\uE243", FontFamilyKey, FontFamily, scale);
     
        /// <inheritdoc/>
        public Glyph FlagThirtySecondDown(double scale) => new("\uE245", FontFamilyKey, FontFamily, scale);
      
        /// <inheritdoc/>
        public Glyph FlagSixtyFourthDown(double scale) => new("\uE247", FontFamilyKey, FontFamily, scale);
     
        /// <inheritdoc/>
        public Glyph NumberZero(double scale) => new("\uE080", FontFamilyKey, FontFamily, scale);
     
        /// <inheritdoc/>
        public Glyph NumberOne(double scale) => new("\uE081", FontFamilyKey, FontFamily, scale);
      
        /// <inheritdoc/>
        public Glyph NumberTwo(double scale) => new("\uE082", FontFamilyKey, FontFamily, scale);
      
        /// <inheritdoc/>
        public Glyph NumberThree(double scale) => new("\uE083", FontFamilyKey, FontFamily, scale);
      
        /// <inheritdoc/>
        public Glyph NumberFour(double scale) => new("\uE084", FontFamilyKey, FontFamily, scale);
     
        /// <inheritdoc/>
        public Glyph NumberFive(double scale) => new("\uE085", FontFamilyKey, FontFamily, scale);
      
        /// <inheritdoc/>
        public Glyph NumberSix(double scale) => new("\uE086", FontFamilyKey, FontFamily, scale);
     
        /// <inheritdoc/>
        public Glyph NumberSeven(double scale) => new("\uE087", FontFamilyKey, FontFamily, scale);
      
        /// <inheritdoc/>
        public Glyph NumberEight(double scale) => new("\uE088", FontFamilyKey, FontFamily, scale);
       
        /// <inheritdoc/>
        public Glyph NumberNine(double scale) => new("\uE089", FontFamilyKey, FontFamily, scale);
       
        /// <inheritdoc/>
        public Glyph TimeSignatureCommon(double scale) => new("\uE08A", FontFamilyKey, FontFamily, scale);
      
        /// <inheritdoc/>
        public Glyph TimeSignatureCut(double scale) => new("\uE08B", FontFamilyKey, FontFamily, scale);
    }
}