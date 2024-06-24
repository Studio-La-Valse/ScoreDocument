using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Layout.Templates;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class StaffGroupLayout : IStaffGroupLayout
    {
        public bool Collapsed { get; }
        public int NumberOfStaves { get; }
        public double DistanceToNext { get; }


        public StaffGroupLayout(int numberOfStaves, double distanceToNext, bool collapsed)
        {
            NumberOfStaves = numberOfStaves;
            DistanceToNext = distanceToNext;
            Collapsed = collapsed;
        }
    }
}
