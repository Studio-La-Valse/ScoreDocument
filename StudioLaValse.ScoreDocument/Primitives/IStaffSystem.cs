namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// The base interface for a staff system.
    /// </summary>
    public interface IStaffSystem
    {

    }

    /// <summary>
    /// The base interface for a staff system.
    /// </summary>
    /// <typeparam name="TStaffGroup"></typeparam>
    /// <typeparam name="TScoreMeasure"></typeparam>
    public interface IStaffSystem<TStaffGroup, TScoreMeasure> : IStaffSystem where TStaffGroup : IStaffGroup
                                                                             where TScoreMeasure : IScoreMeasure
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
