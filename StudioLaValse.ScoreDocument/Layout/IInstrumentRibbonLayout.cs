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
        TemplateProperty<string> DisplayName { get; }

        /// <summary>
        /// Get the 'nickname' for this instrument ribbon.
        /// The default is an abbreviation of the display name.
        /// </summary>
        TemplateProperty<string> AbbreviatedName { get; }

        /// <summary>
        /// Get the collapsed state for this instrument ribbon.
        /// </summary>
        TemplateProperty<Visibility> Visibility { get; }

        /// <summary>
        /// Get or set the number of staves displayed for this instrument.
        /// </summary>
        TemplateProperty<int> NumberOfStaves { get; }

        /// <summary>
        /// Get or set the global scale for this instrument ribbon.
        /// </summary>
        TemplateProperty<double> Scale { get; }
    }
}