namespace StudioLaValse.ScoreDocument.Layout
{
    public interface IScoreDocumentLayout : IScoreElementLayout<IScoreDocumentLayout>
    {
        string Title { get; }
        string SubTitle { get; }
    }
}