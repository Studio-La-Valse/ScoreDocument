namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// The base interface for a staff system.
    /// </summary>
    /// <typeparam name="TStaffGroup"></typeparam>
    /// <typeparam name="TScoreMeasure"></typeparam>
    public interface IStaffSystem<TStaffGroup, TScoreMeasure> : IScoreElement
    {
        /// <summary>
        /// Enumerate the staff groups.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TStaffGroup> EnumerateStaffGroups();
        /// <summary>
        /// Enumerate the measures.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TScoreMeasure> EnumerateMeasures();
    }
}
