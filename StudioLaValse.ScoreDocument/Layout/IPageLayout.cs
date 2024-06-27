using StudioLaValse.ScoreDocument.Templates;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// A page layout.
    /// </summary>
    public interface IPageLayout
    {
        /// <summary>
        /// The margin at the bottom of the page.
        /// </summary>
        double MarginBottom { get; }

        /// <summary>
        /// The margin on the left side of the page.
        /// </summary>
        double MarginLeft { get; }

        /// <summary>
        /// The margin on the right of the page.
        /// </summary>
        double MarginRight { get; }

        /// <summary>
        /// The margin at the top of the page.
        /// </summary>
        double MarginTop { get; }

        /// <summary>
        /// The height of the page (in mm).
        /// </summary>
        int PageHeight { get; }

        /// <summary>
        /// The width of the page (in mm).
        /// </summary>
        int PageWidth { get; }

        /// <summary>
        /// The color of the page.
        /// </summary>
        ColorARGB PageColor { get; }

        /// <summary>
        /// The content color of the page.
        /// </summary>
        ColorARGB ForegroundColor { get; }
    }
}