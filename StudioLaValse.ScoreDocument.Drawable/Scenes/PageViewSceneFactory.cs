namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual score document factory.
    /// </summary>
    public class PageViewSceneFactory : IVisualScoreDocumentContentFactory
    {
        private readonly IVisualStaffSystemFactory staffSystemContentFactory;
        private readonly double smallPadding;
        private readonly double largePadding;
        private readonly IScoreDocumentLayout scoreLayoutDictionary;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="staffSystemContentFactory"></param>
        /// <param name="smallPadding"></param>
        /// <param name="largePadding"></param>
        /// <param name="scoreLayoutDictionary"></param>
        public PageViewSceneFactory(IVisualStaffSystemFactory staffSystemContentFactory, double smallPadding, double largePadding, IScoreDocumentLayout scoreLayoutDictionary)
        {
            this.staffSystemContentFactory = staffSystemContentFactory;
            this.smallPadding = smallPadding;
            this.largePadding = largePadding;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
        }

        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IScoreDocumentReader scoreDocument)
        {
            IList<VisualPage> pages = [];

            var pageCanvasLeft = 0d;
            var globalLineSpacing = GlyphLibrary.LineSpacing;
            foreach (var page in scoreDocument.GeneratePages())
            {
                var pageLayout = scoreLayoutDictionary.PageLayout(page);
                var visualPage = new VisualPage(page, pageCanvasLeft, 0, globalLineSpacing, staffSystemContentFactory, scoreLayoutDictionary);
                pages.Add(visualPage);
                pageCanvasLeft += pageLayout.PageWidth;
                pageCanvasLeft += pages.Count % 2 == 0 ? largePadding : smallPadding;
            }

            return new VisualPageCollection(pages);
        }
    }
}
