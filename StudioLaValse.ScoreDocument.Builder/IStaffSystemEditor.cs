using StudioLaValse.ScoreDocument.Core.Primitives;
using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Builder
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStaffSystemEditor : IStaffSystem<IStaffGroupEditor, IScoreMeasureEditor>, IScoreElementEditor, ILayoutEditor<StaffSystemLayout>
    {

    }
}
