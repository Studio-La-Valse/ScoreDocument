namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// An interface that defines a score element that has children.
    /// </summary>
    public interface IScoreElement
    {
        /// <summary>
        /// Enumerate the children of thie element.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IScoreElement> EnumerateChildren();
    }
}
