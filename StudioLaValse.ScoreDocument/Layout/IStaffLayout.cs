namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// An interface defining a staff layout.
    /// </summary>
    public interface IStaffLayout
    {
        /// <summary>
        /// The distance to the next staff.
        /// </summary>
        double DistanceToNext { get; }
    }
}