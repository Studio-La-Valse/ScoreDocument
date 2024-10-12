using StudioLaValse.ScoreDocument.StyleTemplates;

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
        ReadonlyTemplateProperty<double> MarginBottom { get; }

        /// <summary>
        /// The margin on the left side of the page.
        /// </summary>
        ReadonlyTemplateProperty<double> MarginLeft { get; }

        /// <summary>
        /// The margin on the right of the page.
        /// </summary>
        ReadonlyTemplateProperty<double> MarginRight { get; }

        /// <summary>
        /// The margin at the top of the page.
        /// </summary>
        ReadonlyTemplateProperty<double> MarginTop { get; }

        /// <summary>
        /// The height of the page (in mm).
        /// </summary>
        ReadonlyTemplateProperty<int> PageHeight { get; }

        /// <summary>
        /// The width of the page (in mm).
        /// </summary>
        ReadonlyTemplateProperty<int> PageWidth { get; }

        /// <summary>
        /// The color of the page.
        /// </summary>
        ReadonlyTemplateProperty<ColorARGB> PageColor { get; }

        /// <summary>
        /// The content color of the page.
        /// </summary>
        ReadonlyTemplateProperty<ColorARGB> ForegroundColor { get; }

        /// <summary>
        /// The first system indent.
        /// </summary>
        ReadonlyTemplateProperty<double> FirstSystemIndent { get; }
        /// <summary>
        /// The page scale, inherited from score scale.
        /// </summary>
        ReadonlyTemplateProperty<double> Scale { get; }
    }
}