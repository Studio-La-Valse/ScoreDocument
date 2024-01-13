namespace StudioLaValse.ScoreDocument.Layout
{
    public interface IStaffLayout : IScoreElementLayout<IStaffLayout>
    {
        double LineSpacing { get; }
        double DistanceToNext { get; }
    }
}
