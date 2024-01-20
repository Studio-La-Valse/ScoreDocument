namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// Represents a primitive instrument ribbon.
    /// </summary>
    public interface IInstrumentRibbon : IUniqueScoreElement
    {
        /// <summary>
        /// The instrument of the instrument ribbon.
        /// </summary>
        Instrument Instrument { get; }
        /// <summary>
        /// Enumerate the measures in the ribbon.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IInstrumentMeasure> EnumerateMeasures();
    }
}
