namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// A measure block layout.
    /// </summary>
    public interface IMeasureBlockLayout : ILayout
    {
        /// <summary>
        /// Get or set the steam direction of the measure block.
        /// </summary>
        TemplateProperty<StemDirection> StemDirection { get; }

        /// <summary>
        /// Get or set the stemlength of the measure block.
        /// </summary>
        TemplateProperty<double> StemLength { get; }

        /// <summary>
        /// Get or set the beam angle of the measure block.
        /// </summary>
        TemplateProperty<double> BeamAngle { get; }

        /// <summary>
        /// Get or set the beam thickness of the measure block.
        /// </summary>
        ReadonlyTemplateProperty<double> BeamThickness { get; }

        /// <summary>
        /// Get or set the beam spacing of the measure block.
        /// </summary>
        ReadonlyTemplateProperty<double> BeamSpacing { get; }
    }
}