namespace StudioLaValse.ScoreDocument.Editor
{
    public interface IStaffSystemEditor : IStaffSystem
    {
        IEnumerable<IStaffGroupEditor> EditStaffGroups();
        IStaffGroupEditor EditStaffGroup(int indexInScore);
        void ApplyLayout(IStaffSystemLayout layout);
        IStaffSystemLayout ReadLayout();
    }
}
