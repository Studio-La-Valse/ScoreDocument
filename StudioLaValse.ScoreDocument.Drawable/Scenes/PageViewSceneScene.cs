using StudioLaValse.ScoreDocument.Extensions;
using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual score document factory.
    /// </summary>
    public class PageViewSceneScene : IVisualScoreDocumentScene
    {
        private readonly IVisualPageScene pageFactory;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="pageFactory"></param>
        public PageViewSceneScene(IVisualPageScene pageFactory)
        {
            this.pageFactory = pageFactory;
        }

        /// <inheritdoc/>
        public BaseContentWrapper Create(IScoreDocument scoreDocument)
        {
            IList<BaseContentWrapper> pages = [];

            var pageCanvasLeft = 0d;
            foreach (var page in scoreDocument.ReadPages())
            {
                var pageLayout = page;
                var visualPage = pageFactory.Create(page, pageCanvasLeft, 0);
                pages.Add(visualPage);
                pageCanvasLeft += page.PageWidth;
                pageCanvasLeft += pages.Count % 2 == 0 ? 5 : 10;
            }

            return new VisualPageCollection(pages);
        }
    }
}
