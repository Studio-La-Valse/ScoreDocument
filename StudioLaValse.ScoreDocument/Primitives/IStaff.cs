namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// Represents a staff.
    /// </summary>
    public interface IStaff : IUniqueScoreElement
    {
        /// <summary>
        /// The index of the staff in the parent staffgroup.
        /// </summary>
        int IndexInStaffGroup { get; }
    }
}
