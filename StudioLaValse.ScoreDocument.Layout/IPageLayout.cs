namespace StudioLaValse.ScoreDocument.Layout
{
    public interface IPageLayout
    {
        double MarginBottom { get; }
        double MarginLeft { get; }
        double MarginRight { get; }
        double MarginTop { get; }
        int PageHeight { get; }
        int PageWidth { get; }
    }
}