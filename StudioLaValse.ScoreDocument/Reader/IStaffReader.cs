namespace StudioLaValse.ScoreDocument.Reader
{
    public interface IStaffReader : IStaff
    {
        IStaffLayout ReadLayout();
    }
}
