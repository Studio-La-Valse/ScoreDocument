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
        KeySignature KeySignature { get; set; }

        /// <summary>
        /// Reset the key signature to its default value.
        /// </summary>
        void ResetKeySignature();

        /// <summary>
        /// Applies inner padding to the left side of the measure. Absolute value that does not change with scaling.
        /// </summary>
        double PaddingLeft { get; set; }

        /// <summary>
        /// Reset the padding left to its default value.
        /// </summary>
        void ResetPaddingLeft();

        /// <summary>
        /// Applies inner padding to the right side of the measure. Absolute value that does not change with scaling.
        /// </summary>
        double PaddingRight { get; set; }

        /// <summary>
        /// Reset the padding left to the default value.
        /// </summary>
        void ResetPaddingRight();

        /// <summary>
        /// Requests padding bottom for this measure in the staff system. 
        /// If no value is applied, no padding is requested. 
        /// The highest non-null value of all measures in the staff system will determine the padding of the staff system.
        /// If no non-null values in the staff system are found, the staff system style template will determine the padding below the staff system.
        /// </summary>
        double? PaddingBottom { get; set; }
    }
}