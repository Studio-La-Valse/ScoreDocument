using StudioLaValse.ScoreDocument.Core.Primitives;
using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Builder
{
    /// <summary>
    /// A page in a score doccument.
    /// </summary>
    public interface IPageEditor : IPage<IStaffSystemEditor>, IScoreElementEditor, ILayoutEditor<PageLayout>
    {

    }
}
