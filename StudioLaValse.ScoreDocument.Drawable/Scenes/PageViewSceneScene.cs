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
        private readonly IPositionDictionaryBuilder positionDictionaryBuilder;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="pageFactory"></param>
        /// <param name="positionDictionaryBuilder"></param>
        public PageViewSceneScene(IVisualPageScene pageFactory, IPositionDictionaryBuilder positionDictionaryBuilder)
        {
            this.pageFactory = pageFactory;
            this.positionDictionaryBuilder = positionDictionaryBuilder;
        }

        /// <inheritdoc/>
        public BaseContentWrapper Create(IScoreDocument scoreDocument)
        {
            IList<BaseContentWrapper> pages = [];

            var pageCanvasLeft = 0d;
            foreach (var page in scoreDocument.ReadPages(positionDictionaryBuilder))
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
