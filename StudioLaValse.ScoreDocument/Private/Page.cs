using StudioLaValse.ScoreDocument.Layout;
using ColorARGB = StudioLaValse.ScoreDocument.Templates.ColorARGB;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class Page : IPage
    {
        public IList<IStaffSystem> StaffSystems { get; } = [];
        public IPageLayout Layout { get; }
        public int IndexInScore { get; }

        public double MarginBottom => Layout.MarginBottom;
        public double MarginLeft => Layout.MarginLeft;
        public double MarginRight => Layout.MarginRight;
        public double MarginTop => Layout.MarginTop;
        public int PageHeight => Layout.PageHeight;
        public int PageWidth => Layout.PageWidth;
        public ColorARGB PageColor => Layout.PageColor;
        public ColorARGB ForegroundColor => Layout.ForegroundColor;


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

