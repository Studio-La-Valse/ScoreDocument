using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Layout.Templates;

namespace StudioLaValse.ScoreDocument.Reader.Private
{
    internal class PageLayout : IPageLayout
    {
        private readonly IScoreDocumentLayout documentLayout;

        public int PageWidth => documentLayout.PageWidth;
        public int PageHeight => documentLayout.PageHeight;
        public double MarginLeft => documentLayout.PageMarginLeft;
        public double MarginTop => documentLayout.PageMarginTop;
        public double MarginRight => documentLayout.PageMarginRight;
        public double MarginBottom => documentLayout.PageMarginBottom;
        public ColorARGB PageColor => documentLayout.PageColor;
        public ColorARGB ForegroundColor => documentLayout.PageForegroundColor;


        public PageLayout(IScoreDocumentLayout documentLayout)
        {
            this.documentLayout = documentLayout;
        }
    }
}