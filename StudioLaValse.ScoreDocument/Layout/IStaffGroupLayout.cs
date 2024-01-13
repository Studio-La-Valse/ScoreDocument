namespace StudioLaValse.ScoreDocument.Layout
{
    public interface IStaffGroupLayout : IScoreElementLayout<IStaffGroupLayout>
    {
        double DistanceToNext { get; }
        int NumberOfStaves { get; }
        bool Collapsed { get; }
    }
}
