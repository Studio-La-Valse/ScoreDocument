using StudioLaValse.ScoreDocument.StyleTemplates;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// A staff group layout.
    /// </summary>
    public interface IStaffGroupLayout
    {
        /// <summary>
        /// Boolean value wether the staffgroup is collapsed.
        /// </summary>
        ReadonlyTemplateProperty<bool> Collapsed { get; }

        /// <summary>
        /// The distance to the next staff group.
        /// </summary>
        ReadonlyTemplateProperty<double> DistanceToNext { get; }

        /// <summary>
        /// The number of staves in the staff group.
        /// </summary>
        ReadonlyTemplateProperty<int> NumberOfStaves { get; }

        /// <summary>
        /// Vertical line thickness.
        /// </summary>
        ReadonlyTemplateProperty<double> VerticalStaffLineThickness { get; }

        /// <summary>
        /// The horizontal staff line thickness.
        /// </summary>
        ReadonlyTemplateProperty<double> HorizontalStaffLineThickness { get; }

        /// <summary>
        /// The scale of the staff system.
        /// </summary>
        ReadonlyTemplateProperty<double> Scale { get; }

        /// <summary>
        /// Get the color.
        /// </summary>
        ReadonlyTemplateProperty<ColorARGB> Color { get; }
    }
}