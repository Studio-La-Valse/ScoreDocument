using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// The base interface for staves.
    /// </summary>
    public interface IStaff : IScoreElement, IStaffLayout
    {
        /// <summary>
        /// The index in the staff group.
        /// </summary>
        int IndexInStaffGroup { get; }
    }
}
