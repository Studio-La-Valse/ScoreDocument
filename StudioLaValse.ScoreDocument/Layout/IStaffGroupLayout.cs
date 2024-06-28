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
        ReadonlyTemplateProperty<bool> Collapsed { get; }

        /// <summary>
        /// The distance to the next staff group.
        /// </summary>
        ReadonlyTemplateProperty<double> DistanceToNext { get; }

        /// <summary>
        /// The number of staves in the staff group.
        /// </summary>
        ReadonlyTemplateProperty<int> NumberOfStaves { get; }
    }
}