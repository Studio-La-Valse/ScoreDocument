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
    }
}
