namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// The default implementation of the visual score document factory.
    /// </summary>
    public class PageViewSceneFactory : IVisualScoreDocumentContentFactory
    {
        private readonly IVisualStaffSystemFactory staffSystemContentFactory;
        private readonly PageSize pageSize;
        private readonly double smallPadding;
        private readonly double largePadding;
        private readonly ColorARGB foregroundColor;
        private readonly ColorARGB pageColor;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="staffSystemContentFactory"></param>
        /// <param name="pageSize"></param>
        /// <param name="smallPadding"></param>
        /// <param name="largePadding"></param>
        /// <param name="foregroundColor"></param>
        /// <param name="pageColor"></param>
        public PageViewSceneFactory(IVisualStaffSystemFactory staffSystemContentFactory, PageSize pageSize, double smallPadding, double largePadding, ColorARGB foregroundColor, ColorARGB pageColor)
        {
            this.staffSystemContentFactory = staffSystemContentFactory;
            this.pageSize = pageSize;
            this.smallPadding = smallPadding;
            this.largePadding = largePadding;
            this.foregroundColor = foregroundColor;
            this.pageColor = pageColor;
        }

        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IScoreDocumentReader scoreDocument)
        {
            var pages = new List<VisualPage>()
            {
                new VisualPage(pageSize, 0, 0, staffSystemContentFactory, foregroundColor, pageColor)
            };

            var systemBottom = VisualPage.MarginTop;
            var pageCanvasLeft = 0d;

            foreach (var system in scoreDocument.ReadStaffSystems())
            {
                systemBottom += system.ReadLayout().PaddingTop + system.CalculateHeight();

                if (systemBottom > pageSize.Height - VisualPage.MarginTop)
                {
                    pageCanvasLeft += pageSize.Width;
                    pageCanvasLeft += pages.Count % 2 == 0 ? largePadding : smallPadding;

                    var visualPage = new VisualPage(pageSize, pageCanvasLeft, 0, staffSystemContentFactory, foregroundColor, pageColor);
                    pages.Add(visualPage);

                    systemBottom = VisualPage.MarginTop + system.ReadLayout().PaddingTop + system.CalculateHeight();
                }

                pages.Last().AddSystem(system);
            }

            return new VisualPageCollection(pages);
        }
    }
}
