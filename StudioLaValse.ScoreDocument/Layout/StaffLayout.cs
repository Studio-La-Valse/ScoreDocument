namespace StudioLaValse.ScoreDocument.Layout
{
    public class StaffLayout : IStaffLayout
    {
        public double DistanceToNext { get; }
        public double LineSpacing { get; }

        public StaffLayout(double distanceToNext = 13, double lineSpacing = 1.2)
        {
            DistanceToNext = distanceToNext;
            LineSpacing = lineSpacing;
        }

        public IStaffLayout Copy()
        {
            return new StaffLayout(DistanceToNext, LineSpacing);
        }
    }
}
