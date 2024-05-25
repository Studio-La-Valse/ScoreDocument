using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Layout.Templates;

namespace StudioLaValse.ScoreDocument.Reader.Private
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
