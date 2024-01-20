namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// A factory interface for creating a visual score document.
    /// </summary>
    public interface IVisualScoreDocumentContentFactory
    {
        /// <summary>
        /// Create the visual score document.
        /// </summary>
        /// <param name="scoreDocument"></param>
        /// <returns></returns>
        BaseContentWrapper CreateContent(IScoreDocumentReader scoreDocument);
    }
}
