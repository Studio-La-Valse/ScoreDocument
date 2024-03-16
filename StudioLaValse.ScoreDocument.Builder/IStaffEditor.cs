using StudioLaValse.ScoreDocument.Core.Primitives;
using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Builder
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStaffEditor : IStaff, IScoreElementEditor, ILayoutEditor<StaffLayout>
    {

    }
}
