using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Models.Classes;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class PageLayout : IPageLayout
    {
        private readonly int indexInScore;
        private readonly IScoreDocument scoreDocument;

        public ReadonlyTemplateProperty<int> PageWidth => scoreDocument.PageWidth;
        public ReadonlyTemplateProperty<int> PageHeight => scoreDocument.PageHeight;
        public ReadonlyTemplateProperty<double> MarginLeft => scoreDocument.PageMarginLeft;
        public ReadonlyTemplateProperty<double> MarginTop => scoreDocument.PageMarginTop;
        public ReadonlyTemplateProperty<double> MarginRight => scoreDocument.PageMarginRight;
        public ReadonlyTemplateProperty<double> MarginBottom => scoreDocument.PageMarginBottom;
        public ReadonlyTemplateProperty<ColorARGBClass> PageColor => scoreDocument.PageColor;
        public ReadonlyTemplateProperty<ColorARGBClass> ForegroundColor => scoreDocument.PageForegroundColor;
        public ReadonlyTemplateProperty<double> FirstSystemIndent => new ReadonlyTemplatePropertyFromFunc<double>(() => indexInScore == 0 ? scoreDocument.FirstSystemIndent : 0);
        public ReadonlyTemplateProperty<double> Scale => scoreDocument.Scale;

        public PageLayout(int indexInScore, IScoreDocument scoreDocument)
        {
            this.indexInScore = indexInScore;
            this.scoreDocument = scoreDocument;
        }
    }
}