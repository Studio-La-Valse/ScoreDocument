using StudioLaValse.Drawable.ContentWrappers;
using StudioLaValse.Drawable.DrawableElements;
using StudioLaValse.Geometry;
using StudioLaValse.ScoreDocument.Drawable.Models;
using StudioLaValse.ScoreDocument.Drawable.Scenes;
using StudioLaValse.ScoreDocument.Extensions;
using StudioLaValse.ScoreDocument.Reader;


namespace StudioLaValse.ScoreDocument.Drawable.ContentWrappers
{
    public sealed class VisualPage : BaseContentWrapper
    {
        private readonly PageSize pageSize;
        private readonly double canvasLeft;
        private readonly double canvasTop;
        private readonly IVisualStaffSystemFactory staffSystemContentFactory;
        private readonly ColorARGB foregroundColor;
        private readonly ColorARGB pageColor;
        private readonly List<IStaffSystemReader> content;


        public static double MarginLeft => 20;
        public static double MarginTop => 20;



        public VisualPage(PageSize pageSize, double canvasLeft, double canvasTop, IVisualStaffSystemFactory staffSystemContentFactory, ColorARGB foregroundColor, ColorARGB pageColor)
        {
            this.pageSize = pageSize;
            this.canvasLeft = canvasLeft;
            this.canvasTop = canvasTop;
            this.staffSystemContentFactory = staffSystemContentFactory;
            this.foregroundColor = foregroundColor;
            this.pageColor = pageColor;
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
                canvasTop += staffSystem.ReadLayout().PaddingTop;

                var visualSystem = staffSystemContentFactory.CreateContent(staffSystem, canvasLeft + MarginLeft, canvasTop, pageSize.Width - MarginLeft * 2, foregroundColor);
                yield return visualSystem;

                canvasTop += staffSystem.CalculateHeight();
            }
        }
    }
}
