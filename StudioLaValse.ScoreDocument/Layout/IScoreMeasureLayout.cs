namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// An interface defining a score measure layout.
    /// </summary>
    public interface IScoreMeasureLayout : ILayout
    {
        /// <summary>
        /// Assign the specified key signature to the score measure.
        /// </summary>
        TemplateProperty<KeySignature> KeySignature { get; }

        /// <summary>
        /// Applies inner padding to the left side of the measure. Absolute value that does not change with scaling.
        /// </summary>
        ReadonlyTemplateProperty<double> PaddingLeft { get; }

        /// <summary>
        /// Applies inner padding to the right side of the measure. Absolute value that does not change with scaling.
        /// </summary>
        ReadonlyTemplateProperty<double> PaddingRight { get; }

        /// <summary>
        /// The scale of the measure.
        /// </summary>
        ReadonlyTemplateProperty<double> Scale { get; }

        /// <summary>
        /// Requests padding bottom for this measure in the staff system. 
        /// If no value is applied, no padding is requested. 
        /// The highest non-null value of all measures in the staff system will determine the padding of the staff system.
        /// If no non-null values in the staff system are found, the staff system style template will determine the padding below the staff system.
        /// </summary>
        TemplateProperty<double?> PaddingBottom { get; }
    }
}