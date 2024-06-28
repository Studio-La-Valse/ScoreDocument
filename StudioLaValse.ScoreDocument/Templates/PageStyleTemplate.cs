using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Templates
{
    /// <summary>
    /// A page style template.
    /// </summary>
    public class PageStyleTemplate
    {
        /// <summary>
        /// The page height (in mm)
        /// </summary>
        public required int PageHeight { get; set; }

        /// <summary>
        /// The page width (in mm)
        /// </summary>
        public required int PageWidth { get; set; }

        /// <summary>
        /// The space before content starts (in mm)
        /// </summary>
        public required double MarginTop { get; set; }

        /// <summary>
        /// The right margin of the page (in mm)
        /// </summary>
        public required double MarginRight { get; set; }

        /// <summary>
        /// The space before content starts from the left of the page (in mm)
        /// </summary>
        public required double MarginLeft { get; set; }

        /// <summary>
        /// The space at the bottom of the page.
        /// </summary>
        public required double MarginBottom { get; set; }

        /// <summary>
        /// The page color.
        /// </summary>
        public required ColorARGB PageColor { get; set; }

        /// <summary>
        /// The page content color.
        /// </summary>
        public required ColorARGB ForegroundColor { get; set; }

        /// <summary>
        /// Create the default style template.
        /// </summary>
        /// <returns></returns>
        public static PageStyleTemplate Create()
        {
            return new PageStyleTemplate()
            {
                PageHeight = PageSize.A4.Height,
                PageWidth = PageSize.A4.Width,
                MarginTop = 20,
                MarginRight = 20,
                MarginLeft = 15,
                MarginBottom = 15,
                PageColor = new ColorARGB() { A = 255, R = 214, G = 187, B = 137 },
                ForegroundColor = new ColorARGB() { A = 255, R = 18, G = 79, B = 89 }
            };
        }

        /// <summary>
        /// Apply another style template.
        /// </summary>
        /// <param name="pageStyleTemplate"></param>
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
