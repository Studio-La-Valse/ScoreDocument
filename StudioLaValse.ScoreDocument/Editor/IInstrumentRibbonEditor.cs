namespace StudioLaValse.ScoreDocument.Editor
{
    /// <summary>
    /// Represents an instrument ribbon editor.
    /// </summary>
    public interface IInstrumentRibbonEditor : IInstrumentRibbon
    {
        /// <summary>
        /// The index of the isntrument ribbon in the score.
        /// </summary>
        int IndexInScore { get; }

        /// <summary>
        /// Edit the measures in the instrument ribbon.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IInstrumentMeasureEditor> EditMeasures();
        /// <summary>
        /// Edit the instrument measure at the specified index.
        /// </summary>
        /// <param name="measureIndex"></param>
        /// <returns></returns>
        IInstrumentMeasureEditor EditMeasure(int measureIndex);

        /// <summary>
        /// Applies the specified layout to the instrument ribbon.
        /// </summary>
        /// <param name="layout"></param>
        void ApplyLayout(IInstrumentRibbonLayout layout);
        /// <summary>
        /// Read the layout from the instrument ribbon.
        /// </summary>
        /// <returns></returns>
        IInstrumentRibbonLayout ReadLayout();
    }
}
