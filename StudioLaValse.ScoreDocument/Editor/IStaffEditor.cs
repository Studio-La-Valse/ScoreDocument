using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Editor
{
    public interface IStaffEditor : IStaff
    {
        void ApplyLayout(IStaffLayout layout);
        IStaffLayout ReadLayout();
    }
}
