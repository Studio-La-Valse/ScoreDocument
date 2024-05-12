namespace StudioLaValse.ScoreDocument.Builder
{
    /// <summary>
    /// Represents a score measure editor.
    /// </summary>
    public interface IScoreMeasureEditor : IScoreMeasure<IInstrumentMeasureEditor, IScoreMeasureEditor>, IScoreElementEditor<IScoreMeasureLayout>
    {
        /// <summary>
        /// Assign the specified key signature to the score measure.
        /// </summary>
        /// <param name="keySignature"></param>
        void SetKeySignature(KeySignature keySignature);
        /// <summary>
        /// Applies inner padding to the left side of the measure. Absolute value that does not change with scaling.
        /// </summary>
        /// <param name="padding"></param>
        void SetPaddingLeft(double padding);
        /// <summary>
        /// Applies inner padding to the right side of the measure. Absolute value that does not change with scaling.
        /// </summary>
        /// <param name="padding"></param>
        void SetPaddingRight(double padding);
        /// <summary>
        /// Sets the width of the measure. Can cause the staff sytem to overflow and push measures to the next staff sytem. Absolute value that does not change with scaling.
        /// </summary>
        /// <param name="width"></param>
        void SetWidth(double width);
        /// <summary>
        /// Requests padding bottom for this measure in the staff system. 
        /// If no value is applied, no padding is requested. 
        /// The highest non-null value of all measures in the staff system will determine the padding of the staff system.
        /// If no non-null values in the staff system are found, the staff system style template will determine the padding below the staff system.
        /// </summary>
        /// <param name="padding"></param>
        void SetPaddingBottom(double? padding);
    }
}
