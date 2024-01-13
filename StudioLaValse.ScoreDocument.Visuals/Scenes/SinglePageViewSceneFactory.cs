using StudioLaValse.Drawable.ContentWrappers;
using StudioLaValse.Geometry;
using StudioLaValse.ScoreDocument.Drawable.ContentWrappers;
using StudioLaValse.ScoreDocument.Drawable.Models;
using StudioLaValse.ScoreDocument.Extensions;
using StudioLaValse.ScoreDocument.Reader;

namespace StudioLaValse.ScoreDocument.Drawable.Scenes
{
    public class SinglePageViewSceneFactory : IVisualScoreDocumentContentFactory
    {
        private readonly int pageIndex;
        private readonly PageSize pageSize;
        private readonly IVisualStaffSystemFactory staffSystemContentFactory;
        private readonly ColorARGB foregroundColor;
        private readonly ColorARGB pagecolor;

        public SinglePageViewSceneFactory(int pageIndex, PageSize pageSize, IVisualStaffSystemFactory staffSystemContentFactory, ColorARGB foregroundColor, ColorARGB pageColor)
        {
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
            this.staffSystemContentFactory = staffSystemContentFactory;
            this.foregroundColor = foregroundColor;
            pagecolor = pageColor;
        }

        public BaseContentWrapper CreateContent(IScoreDocumentReader scoreDocument)
        {
            var pages = new List<VisualPage>()
            {
                new VisualPage(pageSize, 0, 0, staffSystemContentFactory, foregroundColor, pagecolor)
            };

            var systemBottom = VisualPage.MarginTop;

            foreach (var system in scoreDocument.ReadStaffSystems())
            {
                systemBottom += system.CalculateHeight() + system.ReadLayout().PaddingTop;

                if (systemBottom > pageSize.Height - VisualPage.MarginTop)
                {
                    var visualPage = new VisualPage(pageSize, 0, 0, staffSystemContentFactory, foregroundColor, pagecolor);
                    pages.Add(visualPage);

                    systemBottom = VisualPage.MarginTop + system.ReadLayout().PaddingTop + system.CalculateHeight();
                }

                pages.Last().AddSystem(system);
            }

            return pages[pageIndex];
        }
    }
}
