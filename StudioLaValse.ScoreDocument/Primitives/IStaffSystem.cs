namespace StudioLaValse.ScoreDocument.Primitives
{
    public interface IStaffSystem : IUniqueScoreElement
    {
        IEnumerable<IScoreMeasure> EnumerateMeasures();
        IEnumerable<IStaffGroup> EnumerateStaffGroups();
    }
}
