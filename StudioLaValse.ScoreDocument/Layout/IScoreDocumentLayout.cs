using StudioLaValse.ScoreDocument.Templates;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// An interface defining a score document layout.
    /// </summary>
    public interface IScoreDocumentLayout 
    {
        /// <summary>
        /// The glyph family.
        /// </summary>
        string GlyphFamily { get; }

        /// <summary>
        /// The first system indent.
        /// </summary>
        double FirstSystemIndent { get; }

        /// <summary>
        /// The horizontal staff line thickness.
        /// </summary>
        double HorizontalStaffLineThickness { get; }

        /// <summary>
        /// The global scale of the score content.
        /// Does not scale pages or page margins.
        /// </summary>
        double Scale { get; }

        /// <summary>
        /// Stem line thickness.
        /// </summary>
        double StemLineThickness { get; }

        /// <summary>
        /// Vertical line thickness.
        /// </summary>
        double VerticalStaffLineThickness { get; }

        /// <summary>
        /// The page color of the score.
        /// </summary>
        ColorARGB PageColor { get; }

        /// <summary>
        /// The page foreground color.
        /// </summary>
        ColorARGB PageForegroundColor { get; }

        /// <summary>
        /// The page margin at the bottom.
        /// </summary>
        double PageMarginBottom { get; }

        /// <summary>
        /// The page margin on the left.
        /// </summary>
        double PageMarginLeft { get; }

        /// <summary>
        /// The page margin on the right.
        /// </summary>
        double PageMarginRight { get; }

        /// <summary>
        /// The page margin at the top.
        /// </summary>
        double PageMarginTop { get; }

        /// <summary>
        /// The height of pages (in mm).
        /// </summary>
        int PageHeight { get; }

        /// <summary>
        /// The page width (in mm).
        /// </summary>
        int PageWidth { get; }

        /// <summary>
        /// The distance from one staff system to the next.
        /// </summary>
        double StaffSystemPaddingBottom { get; }

        /// <summary>
        /// The distance from one staff group to the next.
        /// </summary>
        double StaffGroupPaddingBottom { get; }

        /// <summary>
        /// The distance from one staff to the next.
        /// </summary>
        double StaffPaddingBottom { get; }
    }
}