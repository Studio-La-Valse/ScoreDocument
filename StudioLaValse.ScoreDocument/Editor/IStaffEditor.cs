namespace StudioLaValse.ScoreDocument.Editor
{
    /// <summary>
    /// Represents a staff editor
    /// </summary>
    public interface IStaffEditor : IStaff
    {
        /// <summary>
        /// Apply the layout to the staff editor.
        /// </summary>
        /// <param name="layout"></param>
        void ApplyLayout(IStaffLayout layout);
        /// <summary>
        /// Read the layout from the staff editor.
        /// </summary>
        /// <returns></returns>
        IStaffLayout ReadLayout();
    }
}
