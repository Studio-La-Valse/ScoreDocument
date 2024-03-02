namespace StudioLaValse.ScoreDocument.Core.Primitives
{
    /// <summary>
    /// Represents a staff reader.
    /// </summary>
    public interface IStaff
    {
        /// <summary>
        /// The index of the staff in the parent staffgroup.
        /// </summary>
        int IndexInStaffGroup { get; }
    }
}
