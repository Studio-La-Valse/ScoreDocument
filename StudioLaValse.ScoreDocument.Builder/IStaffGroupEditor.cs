using StudioLaValse.ScoreDocument.Core.Primitives;
using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Builder
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStaffGroupEditor : IStaffGroup<IStaffEditor, IInstrumentRibbonEditor, IInstrumentMeasureEditor>, IScoreElementEditor, ILayoutEditor<StaffGroupLayout>
    {

    }
}
