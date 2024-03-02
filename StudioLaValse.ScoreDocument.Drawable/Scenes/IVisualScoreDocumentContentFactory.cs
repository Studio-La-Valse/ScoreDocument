namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// A factory interface for creating a visual score document.
    /// </summary>
    public interface IVisualScoreDocumentContentFactory
    {
        /// <summary>
        /// Create the visual score document. Please note the return type: <see cref="BaseContentWrapper"/>. Do not return a <see cref="BaseVisualParent{TEntity}"/> where the <see cref="BaseVisualParent{TEntity}.AssociatedElement"/> is the provided score document. The score document should be attached to the scene by the caller of this method.
        /// </summary>
        /// <param name="scoreDocument"></param>
        /// <returns></returns>
        BaseContentWrapper CreateContent(IScoreDocumentReader scoreDocument);
    }
}
