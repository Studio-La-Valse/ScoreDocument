namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal sealed class VisualPage : BaseContentWrapper
    {
        private readonly IPageReader page;
        private readonly double canvasLeft;
        private readonly double canvasTop;
        private readonly IVisualStaffSystemFactory staffSystemContentFactory;
        private readonly ColorARGB foregroundColor;
        private readonly ColorARGB pageColor;
        private readonly IScoreLayoutProvider scoreLayoutDictionary;


        public PageLayout Layout => scoreLayoutDictionary.PageLayout(page);
        public double MarginLeft => Layout.MarginLeft.Value;
        public double MarginRight => Layout.MarginRight.Value;
        public double MarginTop => Layout.MarginTop.Value;
        public double PageWidth => Layout.PageWidth.Value;
        public double PageHeight => Layout.PageHeight.Value;


        public VisualPage(IPageReader page, double canvasLeft, double canvasTop, IVisualStaffSystemFactory staffSystemContentFactory, ColorARGB foregroundColor, ColorARGB pageColor, IScoreLayoutProvider scoreLayoutDictionary)
        {
            this.page = page;
            this.canvasLeft = canvasLeft;
            this.canvasTop = canvasTop;
            this.staffSystemContentFactory = staffSystemContentFactory;
            this.foregroundColor = foregroundColor;
            this.pageColor = pageColor;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
        }



        public override BoundingBox BoundingBox()
        {
            return new BoundingBox(canvasLeft, canvasLeft + PageWidth, canvasTop, canvasTop + PageHeight);
        }
        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            return
            [
                new DrawableRectangle(
                    canvasLeft,
                    canvasTop,
                    PageWidth,
                    PageHeight,
                    pageColor,
                    0.15,
                    foregroundColor)
            ];
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            double canvasTop = this.canvasTop + MarginTop;
            foreach (IStaffSystemReader staffSystem in page.EnumerateStaffSystems())
            {
                StaffSystemLayout staffSystemLayout = scoreLayoutDictionary.StaffSystemLayout(staffSystem);
                BaseContentWrapper visualSystem = staffSystemContentFactory.CreateContent(staffSystem, canvasLeft + MarginLeft, canvasTop, PageWidth - (MarginLeft + MarginRight), foregroundColor);
                yield return visualSystem;

                canvasTop += staffSystem.CalculateHeight(scoreLayoutDictionary);
                canvasTop += staffSystemLayout.PaddingBottom.Value;
            }
        }
    }
}
