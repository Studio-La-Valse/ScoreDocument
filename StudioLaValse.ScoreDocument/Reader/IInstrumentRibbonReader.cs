namespace StudioLaValse.ScoreDocument.Reader
{
    /// <summary>
    /// Represents an instrument ribbon reader.
    /// </summary>
    public interface IInstrumentRibbonReader : IInstrumentRibbon, IUniqueScoreElement
    {
        /// <summary>
        /// The index in the score.
        /// </summary>
        int IndexInScore { get; }

        /// <summary>
        /// Reads the content of the instrument ribbon.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IInstrumentMeasureReader> ReadMeasures();
        /// <summary>
        /// Reads the intrument measure at the specified index in the score.
        /// Throws an exception if no measure exists at the specified index.
        /// </summary>
        /// <param name="indexInScore"></param>
        /// <returns></returns>
        IInstrumentMeasureReader ReadMeasure(int indexInScore);

        /// <summary>
        /// Reads the layout of the instrument ribbon.
        /// </summary>
        /// <returns></returns>
        IInstrumentRibbonLayout ReadLayout();
    }
}
