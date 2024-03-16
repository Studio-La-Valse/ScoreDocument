using StudioLaValse.ScoreDocument.Core.Primitives;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// A page in a score doccument.
    /// </summary>
    public interface IPageReader : IPage<IStaffSystemReader>, IScoreElementReader
    {

    }
}
