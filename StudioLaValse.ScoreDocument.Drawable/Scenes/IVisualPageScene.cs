namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// A factory to create visual score document pages.
    /// </summary>
    public interface IVisualPageScene
    {
        /// <summary>
        /// Create the visual page.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="canvasLeft"></param>
        /// <param name="canvasTop"></param>
        /// <returns></returns>
        BaseContentWrapper Create(IPage page, double canvasLeft, double canvasTop);
    }
}
