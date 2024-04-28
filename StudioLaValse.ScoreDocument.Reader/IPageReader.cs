using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Primitives;

namespace StudioLaValse.ScoreDocument.Reader
{
    /// <summary>
    /// A page in a score doccument.
    /// </summary>
    public interface IPageReader : IPage<IStaffSystemReader>, IHasLayout<IPageLayout>
    {

    }
}
