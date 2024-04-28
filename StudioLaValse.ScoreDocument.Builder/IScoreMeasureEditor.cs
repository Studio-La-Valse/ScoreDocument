﻿namespace StudioLaValse.ScoreDocument.Builder
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
        void EditKeySignature(KeySignature keySignature);
    }
}
