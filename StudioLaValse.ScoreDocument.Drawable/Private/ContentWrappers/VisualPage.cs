using StudioLaValse.ScoreDocument.Reader;
using StudioLaValse.ScoreDocument.Reader.Extensions;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal static class GeometryExtensions
    {
        public static ColorARGB FromPrimitive(this Layout.Templates.ColorARGB color)
        {
            return new ColorARGB(color.A, color.R, color.G, color.B);
        }
    }

    internal sealed class VisualPage : BaseContentWrapper
    {
        private readonly IPageReader page;
        private readonly double canvasLeft;
        private readonly double canvasTop;
        private readonly double globalLineSpacing;
        private readonly IVisualStaffSystemFactory staffSystemContentFactory;
        private readonly IScoreDocumentLayout scoreLayoutDictionary;


        public IScoreDocumentLayout DocumentLayout => scoreLayoutDictionary;
        public ColorARGB PageColor => DocumentLayout.PageColor.FromPrimitive();
        public ColorARGB ForegroundColor => DocumentLayout.ForegroundColor.FromPrimitive();
        public IPageLayout Layout => page.ReadLayout();
        public double MarginLeft => Layout.MarginLeft;
        public double MarginRight => Layout.MarginRight;
        public double MarginTop => Layout.MarginTop;
        public double PageWidth => Layout.PageWidth;
        public double PageHeight => Layout.PageHeight;


        public VisualPage(IPageReader page, double canvasLeft, double canvasTop, double globalLineSpacing, IVisualStaffSystemFactory staffSystemContentFactory, IScoreDocumentLayout scoreLayoutDictionary)
        {
            this.page = page;
            this.canvasLeft = canvasLeft;
            this.canvasTop = canvasTop;
            this.globalLineSpacing = globalLineSpacing;
            this.staffSystemContentFactory = staffSystemContentFactory;
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
                var canvasLeft = this.canvasLeft + MarginLeft;
                if (staffSystem.EnumerateMeasures().First().IndexInScore == 0)
                {
                    canvasLeft += scoreLayoutDictionary.FirstSystemIndent;
                }

                var canvasRight = this.canvasLeft + PageWidth - MarginRight;
                var length = canvasRight - canvasLeft;
                var measureLengthSum = staffSystem.EnumerateMeasures().Select(m => m.ReadLayout().Width).Sum();
                length = Math.Min(length, measureLengthSum);

                var staffSystemLayout = staffSystem.ReadLayout();
                var visualSystem = staffSystemContentFactory.CreateContent(staffSystem, canvasLeft, canvasTop, length, globalLineSpacing, ForegroundColor);
                yield return visualSystem;

                canvasTop += staffSystem.CalculateHeight(globalLineSpacing, scoreLayoutDictionary);
                canvasTop += staffSystemLayout.PaddingBottom;
            }
        }
    }
}
