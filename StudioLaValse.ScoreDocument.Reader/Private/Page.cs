using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Layout.Templates;

namespace StudioLaValse.ScoreDocument.Reader.Private
{
    internal class Page : IPageReader
    {
        public IList<IStaffSystemReader> StaffSystems { get; } = [];
        public IPageLayout Layout { get; }
        public int IndexInScore { get; }




        public Page(int indexInScore, IScoreDocumentLayout scoreDocumentLayout)
        {
            IndexInScore = indexInScore;

            Layout = new PageLayout(scoreDocumentLayout);
        }

        public IEnumerable<IStaffSystemReader> EnumerateStaffSystems()
        {
            return StaffSystems;
        }

        public IPageLayout ReadLayout()
        {
            return Layout;
        }
    }
}

