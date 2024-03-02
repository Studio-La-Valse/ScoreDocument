namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// A staff layout.
    /// </summary>
    public class StaffLayout : ILayoutElement<StaffLayout>
    {

        /// <inheritdoc/>
        public double DistanceToNext { get; set; }
        /// <inheritdoc/>
        public double LineSpacing { get; set; }


        /// <summary>
        /// Create a default staff layout.
        /// </summary>
        /// <param name="distanceToNext"></param>
        /// <param name="lineSpacing"></param>
        public StaffLayout(double distanceToNext = 13, double lineSpacing = 1.2)
        {
            DistanceToNext = distanceToNext;
            LineSpacing = lineSpacing;
        }




        /// <inheritdoc/>
        public StaffLayout Copy()
        {
            return new StaffLayout(DistanceToNext, LineSpacing);
        }
    }
}
