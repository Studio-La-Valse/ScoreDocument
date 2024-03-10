namespace StudioLaValse.ScoreDocument.Layout.Templates
{
    public class PageStyleTemplate
    {
        public int PageHeight { get; set; } = PageSize.A4.Height;
        public int PageWidth { get; set; } = PageSize.A4.Width;
        public double MarginTop { get; set; } = 20;
        public double MarginRight { get; set; } = 5;
        public double MarginLeft { get; set; } = 10;
        public double MarginBottom { get; set; } = 10;
    }
}
