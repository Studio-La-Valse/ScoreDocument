using StudioLaValse.ScoreDocument.Layout;
using ColorARGB = StudioLaValse.ScoreDocument.Templates.ColorARGB;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class PageLayout : IPageLayout
    {
        private readonly IScoreDocument documentLayout;

        public int PageWidth => documentLayout.PageWidth;
        public int PageHeight => documentLayout.PageHeight;
        public double MarginLeft => documentLayout.PageMarginLeft;
        public double MarginTop => documentLayout.PageMarginTop;
        public double MarginRight => documentLayout.PageMarginRight;
        public double MarginBottom => documentLayout.PageMarginBottom;
        public ColorARGB PageColor => documentLayout.PageColor;
        public ColorARGB ForegroundColor => documentLayout.PageForegroundColor;


        public PageLayout(IScoreDocument documentLayout)
        {
            this.documentLayout = documentLayout;
        }
    }
}