namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// A factory interface for creating a visual rest.
    /// </summary>
    public interface IVisualRestFactory
    {
        /// <summary>
        /// Create the visual rest.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="canvasLeft"></param>
        /// <param name="canvasTop"></param>
        /// <returns></returns>
        BaseContentWrapper CreateContent(IChord element, double canvasLeft, double canvasTop);
    }
}
