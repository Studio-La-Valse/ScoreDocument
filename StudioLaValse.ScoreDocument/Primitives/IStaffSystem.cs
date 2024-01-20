namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// Represents a primitive staff system.
    /// </summary>
    public interface IStaffSystem : IUniqueScoreElement
    {
        /// <summary>
        /// Enumerates the score measures in the staff system.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IScoreMeasure> EnumerateMeasures();
        /// <summary>
        /// Enumerates the staff groups in the staff system.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IStaffGroup> EnumerateStaffGroups();
    }
}
