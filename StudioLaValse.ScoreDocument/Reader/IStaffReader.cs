using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Reader
{
    public interface IStaffReader : IStaff
    {
        IStaffLayout ReadLayout();
    }
}
