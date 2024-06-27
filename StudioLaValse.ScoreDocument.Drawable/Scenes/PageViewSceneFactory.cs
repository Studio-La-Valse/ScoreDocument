using StudioLaValse.ScoreDocument.Extensions;
using StudioLaValse.ScoreDocument.GlyphLibrary;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual score document factory.
    /// </summary>
    public class PageViewSceneFactory : IVisualScoreDocumentContentFactory
    {
        private readonly IVisualPageFactory pageFactory;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="pageFactory"></param>
        public PageViewSceneFactory(IVisualPageFactory pageFactory)
        {
            this.pageFactory = pageFactory;
        }

        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IScoreDocument scoreDocument)
        {
            IList<BaseContentWrapper> pages = [];

            var pageCanvasLeft = 0d;
            foreach (var page in scoreDocument.ReadPages(Glyph.LineSpacingMm))
            {
                var pageLayout = page;
                var visualPage = pageFactory.CreateContent(page, pageCanvasLeft, 0);
                pages.Add(visualPage);
                pageCanvasLeft += visualPage.BoundingBox().Width;
                pageCanvasLeft += pages.Count % 2 == 0 ? 5 : 10;
            }

            return new VisualPageCollection(pages);
        }
    }
}
