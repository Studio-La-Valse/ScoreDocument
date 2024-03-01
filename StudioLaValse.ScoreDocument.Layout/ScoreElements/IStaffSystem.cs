using StudioLaValse.ScoreDocument.Core.Primitives;

namespace StudioLaValse.ScoreDocument.Layout.ScoreElements
{
    /// <summary>
    /// Represents a staff system reader.
    /// </summary>
    public interface IStaffSystem : IUniqueScoreElement
    {
        /// <summary>
        /// Reads the score measures contained by this staff.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IScoreMeasureReader> EnumerateMeasures();
        /// <summary>
        /// Reads the staffgroups in this staff system.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IStaffGroup> EnumerateStaffGroups();
    }
}
