namespace StudioLaValse.ScoreDocument.Editor
{
    /// <summary>
    /// Represents a staff system editor.
    /// </summary>
    public interface IStaffSystemEditor : IStaffSystem
    {
        /// <summary>
        /// Edit the staff groups.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IStaffGroupEditor> EditStaffGroups();
        /// <summary>
        /// Edit the staffgroup at the specified index.
        /// </summary>
        /// <param name="indexInScore"></param>
        /// <returns></returns>
        IStaffGroupEditor EditStaffGroup(int indexInScore);
        /// <summary>
        /// Apply the layout to the staff system.
        /// </summary>
        /// <param name="layout"></param>
        void ApplyLayout(IStaffSystemLayout layout);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IStaffSystemLayout ReadLayout();
    }
}
