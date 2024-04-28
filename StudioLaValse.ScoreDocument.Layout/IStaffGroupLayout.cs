namespace StudioLaValse.ScoreDocument.Layout
{
    public interface IStaffGroupLayout
    {
        bool Collapsed { get; }
        double DistanceToNext { get; }
        int NumberOfStaves { get; }
    }
}