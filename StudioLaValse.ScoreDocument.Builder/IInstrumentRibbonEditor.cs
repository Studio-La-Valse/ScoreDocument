namespace StudioLaValse.ScoreDocument.Builder
{
    /// <summary>
    /// Represents an instrument ribbon editor.
    /// </summary>
    public interface IInstrumentRibbonEditor : IInstrumentRibbon<IInstrumentMeasureEditor>, IScoreElementEditor<IInstrumentRibbonLayout>
    {
        /// <summary>
        /// Assign the specified name as the display name for this instrument ribbon.
        /// </summary>
        /// <param name="name"></param>
        void SetDisplayName(string name);
        /// <summary>
        /// Choose a 'nickname' for this instrument ribbon.
        /// </summary>
        /// <param name="abbreviation"></param>
        void SetAbbreviatedName(string abbreviation);
        /// <summary>
        /// Set the number of staves displayed for this instrument.
        /// </summary>
        /// <param name="numberOfStaves"></param>
        void SetNumberOfStaves(int numberOfStaves);
        /// <summary>
        /// Toggle the collapsed state for this instrument ribbon.
        /// </summary>
        /// <param name="isCollapsed"></param>
        void SetCollapsed(bool isCollapsed);
    }
}
