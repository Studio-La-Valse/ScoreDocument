namespace StudioLaValse.ScoreDocument.Reader
{
    /// <summary>
    /// Represents a staff reader.
    /// </summary>
    public interface IStaffReader : IStaff
    {
        /// <summary>
        /// Reads the layout of the staff.
        /// </summary>
        /// <returns></returns>
        IStaffLayout ReadLayout();
    }
}
