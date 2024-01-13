namespace StudioLaValse.ScoreDocument.Primitives
{
    public interface IStaff : IUniqueScoreElement
    {
        int IndexInStaffGroup { get; }
    }
}
