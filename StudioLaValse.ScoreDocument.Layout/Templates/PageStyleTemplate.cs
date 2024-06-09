namespace StudioLaValse.ScoreDocument.Layout.Templates
{
    public class PageStyleTemplate
    {
        public required int PageHeight { get; set; }
        public required int PageWidth { get; set; }
        public required double MarginTop { get; set; }
        public required double MarginRight { get; set; }
        public required double MarginLeft { get; set; }
        public required double MarginBottom { get; set; }
        public required ColorARGB PageColor { get; set; } 
        public required ColorARGB ForegroundColor { get; set; }


        public static PageStyleTemplate Create()
        {
            return new PageStyleTemplate()
            {
                PageHeight = PageSize.A4.Height,
                PageWidth = PageSize.A4.Width,
                MarginTop = 20,
                MarginRight = 5,
                MarginLeft = 10,
                MarginBottom = 10,
                PageColor = new ColorARGB() { A = 255, R = 214, G = 187, B = 137 },
                ForegroundColor = new ColorARGB() { A = 255, R = 0, G = 0, B = 0 }
            };
        }

        public void Apply(PageStyleTemplate pageStyleTemplate)
        {
            PageHeight = pageStyleTemplate.PageHeight;
            PageWidth = pageStyleTemplate.PageWidth;
            MarginTop = pageStyleTemplate.MarginTop;
            MarginRight = pageStyleTemplate.MarginRight;
            MarginLeft = pageStyleTemplate.MarginLeft;
            MarginBottom = pageStyleTemplate.MarginBottom;
            PageColor = pageStyleTemplate.PageColor;
            ForegroundColor = pageStyleTemplate.ForegroundColor;
        }
    }
}
