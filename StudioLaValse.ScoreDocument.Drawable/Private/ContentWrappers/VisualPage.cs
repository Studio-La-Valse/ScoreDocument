using StudioLaValse.ScoreDocument.Drawable.Extensions;
using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Drawable.Private.ContentWrappers
{
    internal sealed class VisualPage : BaseContentWrapper
    {
        private readonly PageSize pageSize;
        private readonly double canvasLeft;
        private readonly double canvasTop;
        private readonly IVisualStaffSystemFactory staffSystemContentFactory;
        private readonly ColorARGB foregroundColor;
        private readonly ColorARGB pageColor;
        private readonly IScoreLayoutDictionary scoreLayoutDictionary;
        private readonly List<IStaffSystemReader> content;


        public static double MarginLeft => 20;
        public static double MarginTop => 20;



        public VisualPage(PageSize pageSize, double canvasLeft, double canvasTop, IVisualStaffSystemFactory staffSystemContentFactory, ColorARGB foregroundColor, ColorARGB pageColor, IScoreLayoutDictionary scoreLayoutDictionary)
        {
            this.pageSize = pageSize;
            this.canvasLeft = canvasLeft;
            this.canvasTop = canvasTop;
            this.staffSystemContentFactory = staffSystemContentFactory;
            this.foregroundColor = foregroundColor;
            this.pageColor = pageColor;
            this.scoreLayoutDictionary = scoreLayoutDictionary;
            content = [];
        }


        public void AddSystem(IStaffSystemReader staffSystem)
        {
            content.Add(staffSystem);
        }


        public override BoundingBox BoundingBox()
        {
            return new BoundingBox(canvasLeft, canvasLeft + pageSize.Width, canvasTop, canvasTop + pageSize.Height);
        }
        public override IEnumerable<BaseDrawableElement> GetDrawableElements()
        {
            return new List<BaseDrawableElement>()
            {
                new DrawableRectangle(
                    canvasLeft,
                    canvasTop,
                    pageSize.Width,
                    pageSize.Height,
                    pageColor,
                    0.15,
                    foregroundColor)
            };
        }
        public override IEnumerable<BaseContentWrapper> GetContentWrappers()
        {
            var canvasTop = this.canvasTop + MarginTop;
            foreach (var staffSystem in content)
            {
                var staffSystemLayout = scoreLayoutDictionary.StaffSystemLayout(staffSystem);
                canvasTop += staffSystemLayout.PaddingTop;

                var visualSystem = staffSystemContentFactory.CreateContent(staffSystem, canvasLeft + MarginLeft, canvasTop, pageSize.Width - MarginLeft * 2, foregroundColor);
                yield return visualSystem;

                canvasTop += staffSystem.CalculateHeight(scoreLayoutDictionary);
            }
        }
    }
}
