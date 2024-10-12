using StudioLaValse.ScoreDocument.Extensions;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal sealed class VisualPage : BaseContentWrapper
    {
        private readonly IPage page;
        private readonly double canvasLeft;
        private readonly double canvasTop;
        private readonly IVisualStaffSystemFactory staffSystemContentFactory;

        public ColorARGB PageColor => page.PageColor.Value.FromPrimitive();
        public ColorARGB ForegroundColor => page.ForegroundColor.Value.FromPrimitive();
        public IPageLayout Layout => page;
        public double MarginLeft => Layout.MarginLeft;
        public double MarginRight => Layout.MarginRight;
        public double MarginTop => Layout.MarginTop;
        public double PageWidth => Layout.PageWidth;
        public double PageHeight => Layout.PageHeight;


        public VisualPage(IPage page,
                          double canvasLeft,
                          double canvasTop,
                          IVisualStaffSystemFactory staffSystemContentFactory)
        {
            this.page = page;
            this.canvasLeft = canvasLeft;
            this.canvasTop = canvasTop;
            this.staffSystemContentFactory = staffSystemContentFactory;
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
                    PageColor,
                    0.05,
                    ForegroundColor)
            ];
        }

        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            foreach (var (staffSystem, canvasTop) in page.EnumerateFromTop(this.canvasTop + MarginTop))
            {
                if (!staffSystem.EnumerateMeasures().Any())
                {
                    throw new Exception("An empty staff system is not allowed.");
                }

                var canvasLeft = this.canvasLeft + MarginLeft;
                if (staffSystem.EnumerateMeasures().First().IndexInScore == 0)
                {
                    canvasLeft += page.FirstSystemIndent;
                }

                var canvasRight = this.canvasLeft + PageWidth - MarginRight;
                var length = canvasRight - canvasLeft;
                var measureLengthSum = staffSystem.EnumerateMeasures().Select(m => m.ApproximateWidth()).Sum();
                length = Math.Min(length, measureLengthSum);

                var staffSystemLayout = staffSystem;
                var visualSystem = staffSystemContentFactory.CreateContent(staffSystem, canvasLeft, canvasTop, length);
                yield return visualSystem;
            }
        }
    }
}
