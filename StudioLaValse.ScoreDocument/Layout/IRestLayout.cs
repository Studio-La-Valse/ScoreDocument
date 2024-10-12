using StudioLaValse.ScoreDocument.StyleTemplates;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// A rest layout.
    /// </summary>
    public interface IRestLayout : ILayout
    {
        /// <summary>
        /// Get the color of the rest.
        /// </summary>
        TemplateProperty<ColorARGB> Color { get; }

        /// <summary>
        /// Define the scale of the rest. Inherited from containing <see cref="IMeasureBlockLayout"/>.
        /// </summary>
        ReadonlyTemplateProperty<double> Scale { get; }

        /// <summary>
        /// The staff on which to place the rest.
        /// </summary>
        TemplateProperty<int> StaffIndex { get; }

        /// <summary>
        /// The staffline on which to place the rest.
        /// </summary>
        TemplateProperty<int> Line { get; }
    }
}