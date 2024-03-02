using StudioLaValse.ScoreDocument.Drawable.Extensions;
using StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers;
using StudioLaValse.ScoreDocument.Layout;

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
        private readonly ColorARGB foregroundColor;
        private readonly ColorARGB pageColor;
        private readonly IScoreLayoutProvider scoreLayoutDictionary;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="staffSystemContentFactory"></param>
        /// <param name="smallPadding"></param>
        /// <param name="largePadding"></param>
        /// <param name="foregroundColor"></param>
        /// <param name="pageColor"></param>
        /// <param name="scoreLayoutDictionary"></param>
        public PageViewSceneFactory(IVisualStaffSystemFactory staffSystemContentFactory, double smallPadding, double largePadding, ColorARGB foregroundColor, ColorARGB pageColor, IScoreLayoutProvider scoreLayoutDictionary)
        {
            this.staffSystemContentFactory = staffSystemContentFactory;
            this.smallPadding = smallPadding;
            this.largePadding = largePadding;
            this.foregroundColor = foregroundColor;
            this.pageColor = pageColor;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
        }

        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IScoreDocumentReader scoreDocument)
        {
            var scoreLayout = scoreLayoutDictionary.DocumentLayout(scoreDocument);

            var pageSize = scoreLayout.PageSize;

            var pages = new List<VisualPage>()
            {
                new VisualPage(pageSize, 0, 0, staffSystemContentFactory, foregroundColor, pageColor, scoreLayoutDictionary)
            };

            var systemBottom = VisualPage.MarginTop;
            var pageCanvasLeft = 0d;

            foreach (var system in scoreDocument.EnumerateStaffSystems())
            {
                var systemLayout = scoreLayoutDictionary.StaffSystemLayout(system);
                systemBottom += systemLayout.PaddingTop + system.CalculateHeight(scoreLayoutDictionary);

                if (systemBottom > pageSize.Height - VisualPage.MarginTop)
                {
                    pageCanvasLeft += pageSize.Width;
                    pageCanvasLeft += pages.Count % 2 == 0 ? largePadding : smallPadding;

                    var visualPage = new VisualPage(pageSize, pageCanvasLeft, 0, staffSystemContentFactory, foregroundColor, pageColor, scoreLayoutDictionary);
                    pages.Add(visualPage);

                    systemBottom = VisualPage.MarginTop + systemLayout.PaddingTop + system.CalculateHeight(scoreLayoutDictionary);
                }

                pages.Last().AddSystem(system);
            }

            return new VisualPageCollection(pages);
        }
    }
}
