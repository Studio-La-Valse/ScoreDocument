namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// The layout of a score document.
    /// </summary>
    public interface IScoreDocumentLayout : IScoreElementLayout<IScoreDocumentLayout>
    {
        /// <summary>
        /// The title of the score.
        /// </summary>
        string Title { get; }
        /// <summary>
        /// The subtitle of the score.
        /// </summary>
        string SubTitle { get; }
    }
}