using StudioLaValse.ScoreDocument.StyleTemplates;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// The staff default staff system layout interface.
    /// </summary>
    public interface IStaffSystemLayout
    {
        /// <summary>
        /// Space below the staff system in mm. Not affected by score scale.
        /// </summary>
        ReadonlyTemplateProperty<double> PaddingBottom { get; }     
        
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