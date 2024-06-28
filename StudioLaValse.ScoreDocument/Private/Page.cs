using StudioLaValse.ScoreDocument.Layout;
using ColorARGB = StudioLaValse.ScoreDocument.Templates.ColorARGB;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class Page : IPage
    {
        public IList<IStaffSystem> StaffSystems { get; } = [];
        public IPageLayout Layout { get; }
        public int IndexInScore { get; }

        public ReadonlyTemplateProperty<double> MarginBottom => Layout.MarginBottom;
        public ReadonlyTemplateProperty<double> MarginLeft => Layout.MarginLeft;
        public ReadonlyTemplateProperty<double> MarginRight => Layout.MarginRight;
        public ReadonlyTemplateProperty<double> MarginTop => Layout.MarginTop;
        public ReadonlyTemplateProperty<int> PageHeight => Layout.PageHeight;
        public ReadonlyTemplateProperty<int> PageWidth => Layout.PageWidth;
        public ReadonlyTemplateProperty<ColorARGB> PageColor => Layout.PageColor;
        public ReadonlyTemplateProperty<ColorARGB> ForegroundColor => Layout.ForegroundColor;


        public Page(int indexInScore, IScoreDocument scoreDocumentLayout)
        {
            IndexInScore = indexInScore;

            Layout = new PageLayout(scoreDocumentLayout);
        }

        public IEnumerable<IStaffSystem> EnumerateStaffSystems()
        {
            return StaffSystems;
        }

        public IEnumerable<IScoreElement> EnumerateChildren()
        {
            return EnumerateStaffSystems();
        }

        public override string ToString()
        {
            return $"Page {IndexInScore}";
        }
    }
}

