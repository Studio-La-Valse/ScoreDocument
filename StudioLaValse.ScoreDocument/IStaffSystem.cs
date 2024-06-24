using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// The base interface for a staff system.
    /// </summary>
    public interface IStaffSystem : IScoreElement, IHasLayout<IStaffSystemLayout>
    {
        /// <summary>
        /// Enumerate the staff groups.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IStaffGroup> EnumerateStaffGroups();
        /// <summary>
        /// Enumerate the measures.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IScoreMeasure> EnumerateMeasures();
    }
}
