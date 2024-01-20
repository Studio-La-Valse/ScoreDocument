namespace StudioLaValse.ScoreDocument.Editor
{
    /// <summary>
    /// Represents the score document editor.
    /// </summary>
    public interface IScoreDocumentEditor : IScoreDocument
    {
        /// <summary>
        /// The number of score measures in the score.
        /// </summary>
        int NumberOfMeasures { get; }
        /// <summary>
        /// The number of instrument ribbons in the score.
        /// </summary>
        int NumberOfInstruments { get; }


        /// <summary>
        /// Edito the score measures.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IScoreMeasureEditor> EditScoreMeasures();
        /// <summary>
        /// Edit the instrument ribbons in the score.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IInstrumentRibbonEditor> EditInstrumentRibbons();
        /// <summary>
        /// Edit the staff systems in the score. 
        /// This is a layout dependent method meaning that the content of the enumerable is differnt when a diffent layout is applied.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IStaffSystemEditor> EditStaffSystems();

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

        /// <summary>
        /// Edit the score measure at the specified index in the score.
        /// If there is no measure at the specified index, an exception will be thrown.
        /// </summary>
        /// <param name="indexInScore"></param>
        /// <returns></returns>
        IScoreMeasureEditor EditScoreMeasure(int indexInScore);
        /// <summary>
        /// Edit the instrument ribbon at the specified index in the score.
        /// If no such element exists at the index, an exception is thrown.
        /// </summary>
        /// <param name="indexInScore"></param>
        /// <returns></returns>
        IInstrumentRibbonEditor EditInstrumentRibbon(int indexInScore);


        /// <summary>
        /// Apply the layout to the score.
        /// </summary>
        /// <param name="layout"></param>
        void ApplyLayout(IScoreDocumentLayout layout);
        /// <summary>
        /// Read the layout from the score.
        /// </summary>
        /// <returns></returns>
        IScoreDocumentLayout ReadLayout();
    }
}
