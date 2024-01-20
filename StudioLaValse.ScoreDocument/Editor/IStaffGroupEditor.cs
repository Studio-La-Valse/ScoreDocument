namespace StudioLaValse.ScoreDocument.Editor
{
    /// <summary>
    /// Represents a staff group editor.
    /// </summary>
    public interface IStaffGroupEditor : IStaffGroup
    {
        /// <summary>
        /// The index of the staffgroup in the staff system.
        /// </summary>
        int IndexInScore { get; }
        /// <summary>
        /// Edit the individual staves in the staffgroup.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IStaffEditor> EditStaves();
        /// <summary>
        /// Edit the staff at the specified index.
        /// </summary>
        /// <param name="staffIndex"></param>
        /// <returns></returns>
        IStaffEditor EditStaff(int staffIndex);
        /// <summary>
        /// Apply the layout to the staffgroup.
        /// </summary>
        /// <param name="layout"></param>
        void ApplyLayout(IStaffGroupLayout layout);
        /// <summary>
        /// Reads the layout of the staff group.
        /// </summary>
        /// <returns></returns>
        IStaffGroupLayout ReadLayout();
    }
}
