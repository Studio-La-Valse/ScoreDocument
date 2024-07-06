using StudioLaValse.ScoreDocument.Layout;
using ColorARGB = StudioLaValse.ScoreDocument.StyleTemplates.ColorARGB;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class PageLayout : IPageLayout
    {
        private readonly IScoreDocument documentLayout;

        public ReadonlyTemplateProperty<int> PageWidth => documentLayout.PageWidth;
        public ReadonlyTemplateProperty<int> PageHeight => documentLayout.PageHeight;
        public ReadonlyTemplateProperty<double> MarginLeft => documentLayout.PageMarginLeft;
        public ReadonlyTemplateProperty<double> MarginTop => documentLayout.PageMarginTop;
        public ReadonlyTemplateProperty<double> MarginRight => documentLayout.PageMarginRight;
        public ReadonlyTemplateProperty<double> MarginBottom => documentLayout.PageMarginBottom;
        public ReadonlyTemplateProperty<ColorARGB> PageColor => documentLayout.PageColor;
        public ReadonlyTemplateProperty<ColorARGB> ForegroundColor => documentLayout.PageForegroundColor;


        public PageLayout(IScoreDocument documentLayout)
        {
            this.documentLayout = documentLayout;
        }
    }
}