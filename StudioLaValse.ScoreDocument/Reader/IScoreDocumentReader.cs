namespace StudioLaValse.ScoreDocument.Reader
{
    /// <summary>
    /// Represents a score document reader.
    /// </summary>
    public interface IScoreDocumentReader : IScoreDocument, IUniqueScoreElement
    {
        /// <summary>
        /// Reads the number of score measures in the score.
        /// </summary>
        int NumberOfMeasures { get; }
        /// <summary>
        /// Reads the number of instrument ribbons. in the score.
        /// </summary>
        int NumberOfInstruments { get; }

        /// <summary>
        /// Reads the score measures in the score.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IScoreMeasureReader> ReadScoreMeasures();
        /// <summary>
        /// Reads the instrument ribbons in the score.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IInstrumentRibbonReader> ReadInstrumentRibbons();
        /// <summary>
        /// Reads the staff systems in the score.
        /// Note that this method is layout dependend: the <see cref="IStaffSystem"/> of each <see cref="IScoreMeasure"/> will be returned if the <see cref="IScoreMeasureLayout"/> has specified that the score measure starts a new system.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IStaffSystemReader> ReadStaffSystems();


        /// <summary>
        /// Read the score measure at the specified index.
        /// </summary>
        /// <param name="indexInScore"></param>
        /// <returns></returns>
        IScoreMeasureReader ReadMeasure(int indexInScore);
        /// <summary>
        /// Read the instrument ribbon at the specified index.
        /// </summary>
        /// <param name="indexInScore"></param>
        /// <returns></returns>
        IInstrumentRibbonReader ReadInstrumentRibbon(int indexInScore);

        /// <summary>
        /// Read the layout of the score.
        /// </summary>
        /// <returns></returns>
        IScoreDocumentLayout ReadLayout();
    }
}
