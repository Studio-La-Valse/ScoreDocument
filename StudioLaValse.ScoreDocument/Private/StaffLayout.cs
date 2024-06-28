using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class StaffLayout : IStaffLayout
    {
        public ReadonlyTemplateProperty<double> DistanceToNext { get; }



        public StaffLayout(ReadonlyTemplateProperty<double> distanceToNext)
        {
            DistanceToNext = distanceToNext;
        }
    }
}
