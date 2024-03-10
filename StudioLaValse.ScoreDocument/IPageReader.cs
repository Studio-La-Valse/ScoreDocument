using StudioLaValse.ScoreDocument.Core.Primitives;
using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// A page in a score doccument. Does not have any attributes or children, but has a GUID so a layout may be attached.
    /// </summary>
    public interface IPageReader : IPage, IScoreEntity
    {
        /// <summary>
        /// Enumerate the staff systems on this page.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IStaffSystemReader> EnumerateStaffSystems();
    }
}
