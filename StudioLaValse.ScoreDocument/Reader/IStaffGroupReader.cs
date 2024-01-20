namespace StudioLaValse.ScoreDocument.Reader
{
    /// <summary>
    /// Represents a staff group reader.
    /// </summary>
    public interface IStaffGroupReader : IStaffGroup
    {
        /// <summary>
        /// Reads the host instrument ribbon.
        /// </summary>
        /// <returns></returns>
        IInstrumentRibbonReader ReadContext();

        /// <summary>
        /// Reads the individual staves in the staffgroup.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IStaffReader> ReadStaves();
        /// <summary>
        /// Reads the instrument measures in the staff group.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IInstrumentMeasureReader> ReadMeasures();

        /// <summary>
        /// Reads the layout of the staffgroup.
        /// </summary>
        /// <returns></returns>
        IStaffGroupLayout ReadLayout();
    }
}
