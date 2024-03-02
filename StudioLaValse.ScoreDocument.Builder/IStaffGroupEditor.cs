using StudioLaValse.ScoreDocument.Core.Primitives;
using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Builder
{
    /// <inheritdoc/>
    public interface IStaffGroupEditor : IStaffGroup<IInstrumentRibbonEditor, IInstrumentMeasureEditor, IStaffEditor>, IScoreElementEditor
    {

    }
}
