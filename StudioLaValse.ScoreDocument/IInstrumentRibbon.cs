using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents an instrument ribbon editor.
    /// </summary>
    public interface IInstrumentRibbon : IHasLayout<IInstrumentRibbonLayout>, IScoreElement, IUniqueScoreElement
    {
        /// <summary>
        /// The index of the isntrument ribbon in the score.
        /// </summary>
        int IndexInScore { get; }
        /// <summary>
        /// The instrument of the instrument ribbon.
        /// </summary>
        Instrument Instrument { get; }

        /// <summary>
        /// Enumerate the measures in the ribbon.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IInstrumentMeasure> ReadMeasures();
        /// <summary>
        /// Edit the instrument measure at the specified index.
        /// </summary>
        /// <param name="measureIndex"></param>
        /// <returns></returns>
        IInstrumentMeasure ReadMeasure(int measureIndex);

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
