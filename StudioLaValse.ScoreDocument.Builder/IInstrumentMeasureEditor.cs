namespace StudioLaValse.ScoreDocument.Builder
{
    /// <summary>
    /// Represents an instrument measure editor.
    /// </summary>
    public interface IInstrumentMeasureEditor : IInstrumentMeasure<IMeasureBlockChainEditor, IInstrumentMeasureEditor>, IScoreElementEditor<IInstrumentMeasureLayout>
    {
        /// <summary>
        /// Clears the content of the instrument measure.
        /// </summary>
        void Clear();
        /// <summary>
        /// Clears the content of one voice in the measure.
        /// </summary>
        /// <param name="voice"></param>
        void RemoveVoice(int voice);
        /// <summary>
        /// Adds a voice to the measure. Does nothing if the voice already exists.
        /// </summary>
        /// <param name="voice"></param>
        void AddVoice(int voice);

        /// <summary>
        /// Add a clefchange to this instrument measure.
        /// </summary>
        /// <param name="clefChange"></param>
        void AddClefChange(ClefChange clefChange);
    }
}
