using StudioLaValse.ScoreDocument.StyleTemplates;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// An interface defining a staff layout.
    /// </summary>
    public interface IStaffLayout
    {
        /// <summary>
        /// The distance to the next staff.
        /// </summary>
        ReadonlyTemplateProperty<double> DistanceToNext { get; }

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