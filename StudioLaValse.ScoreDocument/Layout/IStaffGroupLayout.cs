namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// A staff group layout.
    /// </summary>
    public interface IStaffGroupLayout
    {
        /// <summary>
        /// Boolean value wether the staffgroup is collapsed.
        /// </summary>
        bool Collapsed { get; }

        /// <summary>
        /// The distance to the next staff group.
        /// </summary>
        double DistanceToNext { get; }

        /// <summary>
        /// The number of staves in the staff group.
        /// </summary>
        int NumberOfStaves { get; }
    }
}