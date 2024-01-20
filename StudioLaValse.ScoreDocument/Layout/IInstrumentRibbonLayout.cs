namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// The layout of an instrument ribbon.
    /// </summary>
    public interface IInstrumentRibbonLayout : IScoreElementLayout<IInstrumentRibbonLayout>
    {
        /// <summary>
        /// The nickname of the instrument ribbon.
        /// </summary>
        string AbbreviatedName { get; }
        /// <summary>
        /// Specifies whether the ribbon is collapsed.
        /// </summary>
        bool Collapsed { get; }
        /// <summary>
        /// The full name of the instrument ribbon.
        /// </summary>
        string DisplayName { get; }
        /// <summary>
        /// The default number of staves of the ribbon. 
        /// Will be overriden per staff system by each <see cref="IStaffGroupLayout"/>'s <see cref="IStaffGroupLayout.NumberOfStaves"/>.
        /// </summary>
        int NumberOfStaves { get; }
    }
}