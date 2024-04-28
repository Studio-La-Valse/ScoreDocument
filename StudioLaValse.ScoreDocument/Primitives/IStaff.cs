namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// The base interface for staves.
    /// </summary>
    public interface IStaff
    {
        /// <summary>
        /// The index in the staff group.
        /// </summary>
        int IndexInStaffGroup { get; }
    }
}
