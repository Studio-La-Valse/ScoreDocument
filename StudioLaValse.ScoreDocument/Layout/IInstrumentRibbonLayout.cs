namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// An instrument ribbon layout.
    /// </summary>
    public interface IInstrumentRibbonLayout : ILayout
    {
        /// <summary>
        /// Get the specified name as the display name for this instrument ribbon.
        /// The default is provided by the host instrument.
        /// </summary>
        string DisplayName { get; set; }

        /// <summary>
        /// Set the name to its default unset value.
        /// </summary>
        void ResetDisplayName();

        /// <summary>
        /// Get the 'nickname' for this instrument ribbon.
        /// The default is an abbreviation of the display name.
        /// </summary>
        string AbbreviatedName { get; set; }

        /// <summary>
        /// Set the abbreviated name to its default unset value.
        /// </summary>
        void ResetAbbreviatedName();

        /// <summary>
        /// Get the collapsed state for this instrument ribbon.
        /// </summary>
        bool Collapsed { get; set; }

        /// <summary>
        /// Reset the collapsed state to its default unset value.
        /// </summary>
        void ResetCollapsed();

        /// <summary>
        /// Get or set the number of staves displayed for this instrument.
        /// </summary>
        int NumberOfStaves { get; set; }

        /// <summary>
        /// Reset the number of staves to its default unset value.
        /// </summary>
        void ResetNumberOfStaves();

        /// <summary>
        /// Get or set the global scale for this instrument ribbon.
        /// </summary>
        double Scale { get; set; }

        /// <summary>
        /// Reset the scale to its default unset value.
        /// </summary>
        void ResetScale();
    }
}