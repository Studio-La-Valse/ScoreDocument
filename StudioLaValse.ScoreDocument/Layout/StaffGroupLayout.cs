namespace StudioLaValse.ScoreDocument.Layout
{
    public class StaffGroupLayout : IStaffGroupLayout
    {
        public double DistanceToNext { get; }
        public int NumberOfStaves { get; }
        public bool Collapsed { get; } = false;

        public StaffGroupLayout(Instrument instrument, double distanceToNext = 22, bool collapsed = false) : this(instrument.NumberOfStaves, distanceToNext, collapsed)
        {

        }

        public StaffGroupLayout(int numberOfStaves, double distanceToNext = 22, bool collapsed = false)
        {
            NumberOfStaves = numberOfStaves;
            DistanceToNext = distanceToNext;
            Collapsed = collapsed;
        }

        public IStaffGroupLayout Copy()
        {
            return new StaffGroupLayout(NumberOfStaves, DistanceToNext, Collapsed);
        }
    }
}
