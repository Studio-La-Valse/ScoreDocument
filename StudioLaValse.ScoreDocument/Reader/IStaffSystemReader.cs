namespace StudioLaValse.ScoreDocument.Reader
{
    /// <summary>
    /// Represents a staff system reader.
    /// </summary>
    public interface IStaffSystemReader : IStaffSystem, IUniqueScoreElement
    {
        /// <summary>
        /// Reads the score measures contained by this staff.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IScoreMeasureReader> ReadMeasures();
        /// <summary>
        /// Reads the staffgroups in this staff system.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IStaffGroupReader> ReadStaffGroups();
        /// <summary>
        /// Reads the staffgroup at the specified index.
        /// </summary>
        /// <param name="indexInScore"></param>
        /// <returns></returns>
        IStaffGroupReader ReadStaffGroup(int indexInScore);
        /// <summary>
        /// Reads the layout of the staff system.
        /// </summary>
        /// <returns></returns>
        IStaffSystemLayout ReadLayout();
    }
}
