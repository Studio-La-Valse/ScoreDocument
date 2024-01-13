namespace StudioLaValse.ScoreDocument.Layout
{
    public class ScoreDocumentLayout : IScoreDocumentLayout
    {
        public string Title { get; }
        public string SubTitle { get; }


        public ScoreDocumentLayout(string title, string subTitle)
        {
            Title = title;
            SubTitle = subTitle;
        }

        public IScoreDocumentLayout Copy()
        {
            return new ScoreDocumentLayout(Title, SubTitle);
        }
    }
}