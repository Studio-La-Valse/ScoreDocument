using StudioLaValse.ScoreDocument.Core.Primitives;
using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Builder
{
    /// <inheritdoc/>
    public interface IStaffEditor : IStaff, ILayoutEditor<StaffLayout>, IScoreElementEditor
    {

    }
}
