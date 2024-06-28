using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class StaffGroupLayout : IStaffGroupLayout
    {
        public ReadonlyTemplateProperty<bool> Collapsed { get; }
        public ReadonlyTemplateProperty<int> NumberOfStaves { get; }
        public ReadonlyTemplateProperty<double> DistanceToNext { get; }


        public StaffGroupLayout(ReadonlyTemplateProperty<int> numberOfStaves, ReadonlyTemplateProperty<double> distanceToNext, ReadonlyTemplateProperty<bool> collapsed)
        {
            NumberOfStaves = numberOfStaves;
            DistanceToNext = distanceToNext;
            Collapsed = collapsed;
        }
    }
}
