using StudioLaValse.ScoreDocument.Layout.Templates;
using StudioLaValse.ScoreDocument.Reader;
using StudioLaValse.ScoreDocument.Reader.Extensions;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual score document factory.
    /// </summary>
    public class PageViewSceneFactory : IVisualScoreDocumentContentFactory
    {
        private readonly IVisualPageFactory pageFactory;
        private readonly double smallPadding;
        private readonly double largePadding;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="pageFactory"></param>
        /// <param name="smallPadding"></param>
        /// <param name="largePadding"></param>
        public PageViewSceneFactory(IVisualPageFactory pageFactory, double smallPadding, double largePadding)
        {
            this.pageFactory = pageFactory;
            this.smallPadding = smallPadding;
            this.largePadding = largePadding;
        }

        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IScoreDocumentReader scoreDocument)
        {
            IList<BaseContentWrapper> pages = [];

            var pageCanvasLeft = 0d;
            foreach (var page in scoreDocument.ReadPages())
            {
                var pageLayout = page.ReadLayout();
                var visualPage = pageFactory.CreateContent(page, pageCanvasLeft, 0);
                pages.Add(visualPage);
                pageCanvasLeft += pageLayout.PageWidth;
                pageCanvasLeft += pages.Count % 2 == 0 ? largePadding : smallPadding;
            }

            return new VisualPageCollection(pages);
        }
    }
}
