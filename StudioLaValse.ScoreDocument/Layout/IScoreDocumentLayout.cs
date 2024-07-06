using StudioLaValse.ScoreDocument.StyleTemplates;

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
        ReadonlyTemplateProperty<string> GlyphFamily { get; }

        /// <summary>
        /// The first system indent.
        /// </summary>
        ReadonlyTemplateProperty<double> FirstSystemIndent { get; }

        /// <summary>
        /// The horizontal staff line thickness.
        /// </summary>
        ReadonlyTemplateProperty<double> HorizontalStaffLineThickness { get; }

        /// <summary>
        /// The global scale of the score content.
        /// Does not scale pages or page margins.
        /// </summary>
        ReadonlyTemplateProperty<double> Scale { get; }

        /// <summary>
        /// Stem line thickness.
        /// </summary>
        ReadonlyTemplateProperty<double> StemLineThickness { get; }

        /// <summary>
        /// Vertical line thickness.
        /// </summary>
        ReadonlyTemplateProperty<double> VerticalStaffLineThickness { get; }

        /// <summary>
        /// The page color of the score.
        /// </summary>
        ReadonlyTemplateProperty<ColorARGB> PageColor { get; }

        /// <summary>
        /// The page foreground color.
        /// </summary>
        ReadonlyTemplateProperty<ColorARGB> PageForegroundColor { get; }

        /// <summary>
        /// The page margin at the bottom.
        /// </summary>
        ReadonlyTemplateProperty<double> PageMarginBottom { get; }

        /// <summary>
        /// The page margin on the left.
        /// </summary>
        ReadonlyTemplateProperty<double> PageMarginLeft { get; }

        /// <summary>
        /// The page margin on the right.
        /// </summary>
        ReadonlyTemplateProperty<double> PageMarginRight { get; }

        /// <summary>
        /// The page margin at the top.
        /// </summary>
        ReadonlyTemplateProperty<double> PageMarginTop { get; }

        /// <summary>
        /// The height of pages (in mm).
        /// </summary>
        ReadonlyTemplateProperty<int> PageHeight { get; }

        /// <summary>
        /// The page width (in mm).
        /// </summary>
        ReadonlyTemplateProperty<int> PageWidth { get; }

        /// <summary>
        /// The distance from one staff system to the next.
        /// </summary>
        ReadonlyTemplateProperty<double> StaffSystemPaddingBottom { get; }

        /// <summary>
        /// The distance from one staff group to the next.
        /// </summary>
        ReadonlyTemplateProperty<double> StaffGroupPaddingBottom { get; }

        /// <summary>
        /// The distance from one staff to the next.
        /// </summary>
        ReadonlyTemplateProperty<double> StaffPaddingBottom { get; }
    }
}