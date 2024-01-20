namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// Represents a score measure primitive.
    /// </summary>
    public interface IScoreMeasure : IUniqueScoreElement
    {
        /// <summary>
        /// The time signature of the measure.
        /// </summary>
        TimeSignature TimeSignature { get; }

        /// <summary>
        /// Enumerates the instrument measures of the score.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IInstrumentMeasure> EnumerateMeasures();

        /// <summary>
        /// The staff system origin of the measure.
        /// All measures have a staff system origin. 
        /// The score document enumerates <see cref="IStaffSystem"/>s dependent on the <see cref="IScoreMeasureLayout"/>.
        /// Therefore the specified <see cref="IStaffSystemEditor"/> may not be part of the enumerated staff systems.
        /// </summary>
        /// <returns></returns>
        IStaffSystem GetStaffSystemOrigin();
    }
}
