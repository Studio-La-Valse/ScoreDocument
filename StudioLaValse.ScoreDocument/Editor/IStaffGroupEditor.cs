namespace StudioLaValse.ScoreDocument.Editor
{
    public interface IStaffGroupEditor : IStaffGroup
    {
        int IndexInScore { get; }
        IEnumerable<IStaffEditor> EditStaves();
        IStaffEditor EditStaff(int staffIndex);
        void ApplyLayout(IStaffGroupLayout layout);
        IStaffGroupLayout ReadLayout();
    }
}
