namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// The layout of a staff group.
    /// </summary>
    public class StaffGroupLayout : IStaffGroupLayout
    {
        /// <inheritdoc/>
        public double DistanceToNext { get; }
        /// <inheritdoc/>
        public int NumberOfStaves { get; }
        /// <inheritdoc/>
        public bool Collapsed { get; }

        /// <summary>
        /// Create a new staff group layout.
        /// </summary>
        /// <param name="instrument"></param>
        /// <param name="distanceToNext"></param>
        /// <param name="collapsed"></param>
        public StaffGroupLayout(Instrument instrument, double distanceToNext = 22, bool collapsed = false) : this(instrument.NumberOfStaves, distanceToNext, collapsed)
        {

        }

        /// <summary>
        /// Create a new staff group layout.
        /// </summary>
        /// <param name="numberOfStaves"></param>
        /// <param name="distanceToNext"></param>
        /// <param name="collapsed"></param>
        public StaffGroupLayout(int numberOfStaves, double distanceToNext = 22, bool collapsed = false)
        {
            NumberOfStaves = numberOfStaves;
            DistanceToNext = distanceToNext;
            Collapsed = collapsed;
        }

        /// <inheritdoc/>
        public IStaffGroupLayout Copy()
        {
            return new StaffGroupLayout(NumberOfStaves, DistanceToNext, Collapsed);
        }
    }
}
