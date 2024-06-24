using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Private
{
    internal class Page : IPage
    {
        public IList<IStaffSystem> StaffSystems { get; } = [];
        public IPageLayout Layout { get; }
        public int IndexInScore { get; }




        public Page(int indexInScore, IScoreDocumentLayout scoreDocumentLayout)
        {
            IndexInScore = indexInScore;

            Layout = new PageLayout(scoreDocumentLayout);
        }

        public IEnumerable<IStaffSystem> EnumerateStaffSystems()
        {
            return StaffSystems;
        }

        public IPageLayout ReadLayout()
        {
            return Layout;
        }

        public IEnumerable<IScoreElement> EnumerateChildren()
        {
            return EnumerateStaffSystems();
        }

        public override string ToString()
        {
            return $"Page {IndexInScore}";
        }

        public void RemoveLayout()
        {
            
        }
    }
}

