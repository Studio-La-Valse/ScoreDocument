namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// Represents a primitive score document.
    /// </summary>
    public interface IScoreDocument : IUniqueScoreElement
    {
        /// <summary>
        /// Enumerates the score measures in the score.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IScoreMeasure> EnumerateScoreMeasures();
        /// <summary>
        /// Enumerates the instrument ribbons in the score.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IInstrumentRibbon> EnumerateInstrumentRibbons();
    }
}
