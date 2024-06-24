using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Extensions;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal sealed class VisualPage : BaseContentWrapper
    {
        private readonly IPage page;
        private readonly double canvasLeft;
        private readonly double canvasTop;
        private readonly double globalLineSpacing;
        private readonly IVisualStaffSystemFactory staffSystemContentFactory;
        private readonly IScoreDocumentLayout scoreDocumentLayout;
        private readonly IUnitToPixelConverter unitToPixelConverter;

        public ColorARGB PageColor => page.ReadLayout().PageColor.FromPrimitive();
        public ColorARGB ForegroundColor => page.ReadLayout().ForegroundColor.FromPrimitive();
        public IPageLayout Layout => page.ReadLayout();
        public double MarginLeft => unitToPixelConverter.UnitsToPixels(Layout.MarginLeft);
        public double MarginRight => unitToPixelConverter.UnitsToPixels(Layout.MarginRight);
        public double MarginTop => unitToPixelConverter.UnitsToPixels(Layout.MarginTop);
        public double PageWidth => unitToPixelConverter.UnitsToPixels(Layout.PageWidth);
        public double PageHeight => unitToPixelConverter.UnitsToPixels(Layout.PageHeight);


        public VisualPage(IPage page,
                          double canvasLeft,
                          double canvasTop,
                          double globalLineSpacing,
                          IVisualStaffSystemFactory staffSystemContentFactory,
                          IScoreDocumentLayout scoreDocumentLayout,
                          IUnitToPixelConverter unitToPixelConverter)
        {
            this.page = page;
            this.canvasLeft = canvasLeft;
            this.canvasTop = canvasTop;
            this.globalLineSpacing = globalLineSpacing;
            this.staffSystemContentFactory = staffSystemContentFactory;
            this.scoreDocumentLayout = scoreDocumentLayout;
            this.unitToPixelConverter = unitToPixelConverter;
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
            var canvasTop = this.canvasTop + MarginTop;
            foreach (var staffSystem in page.EnumerateStaffSystems())
            {
                if (!staffSystem.EnumerateMeasures().Any())
                {
                    throw new Exception("An empty staff system is not allowed.");
                }

                var canvasLeft = this.canvasLeft + MarginLeft;
                if (staffSystem.EnumerateMeasures().First().IndexInScore == 0)
                {
                    canvasLeft += scoreDocumentLayout.FirstSystemIndent;
                }

                var canvasRight = this.canvasLeft + PageWidth - MarginRight;
                var length = canvasRight - canvasLeft;
                var scoreScale = scoreDocumentLayout.Scale;
                var measureLengthSum = staffSystem.EnumerateMeasures().Select(m => m.ApproximateWidth(scoreScale)).Sum();
                measureLengthSum = unitToPixelConverter.UnitsToPixels(measureLengthSum);
                length = Math.Min(length, measureLengthSum);

                var staffSystemLayout = staffSystem.ReadLayout();
                var visualSystem = staffSystemContentFactory.CreateContent(staffSystem, canvasLeft, canvasTop, length, globalLineSpacing);
                yield return visualSystem;

                canvasTop += unitToPixelConverter.UnitsToPixels(staffSystem.CalculateHeight(globalLineSpacing, scoreDocumentLayout));
                canvasTop += unitToPixelConverter.UnitsToPixels(staffSystemLayout.PaddingBottom * scoreScale);
            }
        }
    }
}
