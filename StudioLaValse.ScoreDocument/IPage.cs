using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// A page in a score doccument.
    /// </summary>
    public interface IPage : IHasLayout<IPageLayout>
    {
        /// <summary>
        /// The index in the score.
        /// </summary>
        int IndexInScore { get; }

        /// <summary>
        /// Enumerate the staff systems on this page.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IStaffSystem> EnumerateStaffSystems();
    }
}
