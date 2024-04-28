namespace StudioLaValse.ScoreDocument.Builder
{
    /// <summary>
    /// Represents the score document editor.
    /// </summary>
    public interface IScoreDocumentEditor : IScoreDocument<IPageEditor, IScoreMeasureEditor, IInstrumentRibbonEditor>, IScoreElementEditor<IScoreDocumentLayout>
    {
        /// <summary>
        /// Clears all content of the score.
        /// </summary>
        void Clear();
        /// <summary>
        /// Add an instrument ribbon from the instrument at the end of the score.
        /// </summary>
        /// <param name="instrument"></param>
        void AddInstrumentRibbon(Instrument instrument);
        /// <summary>
        /// Remove an instrument ribbon with the specified element id.
        /// Throws an exception if no element with the specified element is found.
        /// </summary>
        /// <param name="indexInScore"></param>
        void RemoveInstrumentRibbon(int indexInScore);
        /// <summary>
        /// Append a score measure to the end of the score. 
        /// If no time signature is specified, the time signature of the previous measure is used.
        /// If there is no previous measure, and no time signature is specified, 4/4 will be used.
        /// </summary>
        /// <param name="timeSignature"></param>
        void AppendScoreMeasure(TimeSignature? timeSignature = null);
        /// <summary>
        /// Insert a score measure at the specified index.
        /// If no time signature is specified, the time signature of the previous measure is used.
        /// If there is no previous measure, and no time signature is specified, 4/4 will be used.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="timeSignature"></param>
        void InsertScoreMeasure(int index, TimeSignature? timeSignature = null);
        /// <summary>
        /// Remove the score measure at the specified index in the score.
        /// If there is no measure at the specified index, an exception will be thrown.
        /// </summary>
        /// <param name="indexInScore"></param>
        void RemoveScoreMeasure(int indexInScore);
    }
}