namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// A page in a score doccument.
    /// </summary>
    public interface IPage 
    {
        /// <summary>
        /// The index in the score.
        /// </summary>
        int IndexInScore { get; }
    }

    /// <summary>
    /// A page in a score doccument.
    /// </summary>
    /// <typeparam name="TStaffSystem">
    /// </typeparam>
    public interface IPage<TStaffSystem> : IPage where TStaffSystem : IStaffSystem
    {
        /// <summary>
        /// Enumerate the staff systems on this page.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TStaffSystem> EnumerateStaffSystems();
    }
}
