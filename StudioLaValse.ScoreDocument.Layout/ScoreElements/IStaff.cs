using StudioLaValse.ScoreDocument.Core.Primitives;

namespace StudioLaValse.ScoreDocument.Layout.ScoreElements
{
    /// <summary>
    /// Represents a staff reader.
    /// </summary>
    public interface IStaff : IUniqueScoreElement
    {
        /// <summary>
        /// The index of the staff in the parent staffgroup.
        /// </summary>
        int IndexInStaffGroup { get; }
    }
}
