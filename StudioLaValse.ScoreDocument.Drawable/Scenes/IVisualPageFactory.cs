using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// A factory to create visual score document pages.
    /// </summary>
    public interface IVisualPageFactory
    {
        /// <summary>
        /// Create the visual page.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="canvasLeft"></param>
        /// <param name="canvasTop"></param>
        /// <returns></returns>
        BaseContentWrapper CreateContent(IPageReader page, double canvasLeft, double canvasTop);
    }
}
