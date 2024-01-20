namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// The layout of a staff group.
    /// </summary>
    public interface IStaffGroupLayout : IScoreElementLayout<IStaffGroupLayout>
    {
        /// <summary>
        /// Specifies how much distance is reserved towards the next staff group.
        /// </summary>
        double DistanceToNext { get; }
        /// <summary>
        /// The number of staves in this staff group.
        /// </summary>
        int NumberOfStaves { get; }
        /// <summary>
        /// Specifies if the staff group is hidden.
        /// </summary>
        bool Collapsed { get; }
    }
}
