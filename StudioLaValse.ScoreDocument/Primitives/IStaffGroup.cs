namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// Represents a primitive staffgroup.
    /// </summary>
    public interface IStaffGroup : IUniqueScoreElement
    {
        /// <summary>
        /// The instrument of the staffgroup.
        /// </summary>
        Instrument Instrument { get; }

        /// <summary>
        /// Enumerate the primitive staves in the staffgroup.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IStaff> EnumerateStaves();
        /// <summary>
        /// Enumerate the instrument measures in the staffgroup.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IInstrumentMeasure> EnumerateMeasures();
    }
}
