using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class StaffLayout : IStaffLayout
    {
        public double DistanceToNext { get; }



        public StaffLayout(double distanceToNext)
        {
            DistanceToNext = distanceToNext;
        }
    }
}
