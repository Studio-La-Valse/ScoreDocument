using StudioLaValse.ScoreDocument.Drawable.Extensions;
using StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers;
using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    /// <summary>
    /// An implementation of the visual score document factory, that visualizes a single page of the score document.
    /// </summary>
    public class SinglePageViewSceneFactory : IVisualScoreDocumentContentFactory
    {
        private readonly int pageIndex;
        private readonly IVisualStaffSystemFactory staffSystemContentFactory;
        private readonly ColorARGB foregroundColor;
        private readonly ColorARGB pagecolor;
        private readonly IScoreLayoutDictionary scoreLayoutDictionary;

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="staffSystemContentFactory"></param>
        /// <param name="foregroundColor"></param>
        /// <param name="pageColor"></param>
        /// <param name="scoreLayoutDictionary"></param>
        public SinglePageViewSceneFactory(int pageIndex, IVisualStaffSystemFactory staffSystemContentFactory, ColorARGB foregroundColor, ColorARGB pageColor, IScoreLayoutDictionary scoreLayoutDictionary)
        {
            this.pageIndex = pageIndex;
            this.staffSystemContentFactory = staffSystemContentFactory;
            this.foregroundColor = foregroundColor;
            this.pagecolor = pageColor;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
        }
        /// <inheritdoc/>
        public BaseContentWrapper CreateContent(IScoreDocumentReader scoreDocument)
        {
            var pageSize = scoreLayoutDictionary.DocumentLayout(scoreDocument).PageSize;

            var pages = new List<VisualPage>()
            {
                new VisualPage(pageSize, 0, 0, staffSystemContentFactory, foregroundColor, pagecolor, scoreLayoutDictionary)
            };

            var systemBottom = VisualPage.MarginTop;

            foreach (var system in scoreDocument.EnumerateStaffSystems())
            {
                var systemLayout = scoreLayoutDictionary.StaffSystemLayout(system);
                systemBottom += system.CalculateHeight(scoreLayoutDictionary) + systemLayout.PaddingTop;

                if (systemBottom > pageSize.Height - VisualPage.MarginTop)
                {
                    var visualPage = new VisualPage(pageSize, 0, 0, staffSystemContentFactory, foregroundColor, pagecolor, scoreLayoutDictionary);
                    pages.Add(visualPage);

                    systemBottom = VisualPage.MarginTop + systemLayout.PaddingTop + system.CalculateHeight(scoreLayoutDictionary);
                }

                pages.Last().AddSystem(system);
            }

            return pages[pageIndex];
        }
    }
}
