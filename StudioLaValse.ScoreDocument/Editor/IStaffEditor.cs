namespace StudioLaValse.ScoreDocument.Editor
{
    public interface IStaffEditor : IStaff
    {
        void ApplyLayout(IStaffLayout layout);
        IStaffLayout ReadLayout();
    }
}
