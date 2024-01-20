namespace StudioLaValse.ScoreDocument.Editor
{
    /// <summary>
    /// Represents an instrument measure editor.
    /// </summary>
    public interface IInstrumentMeasureEditor : IInstrumentMeasure
    {
        /// <summary>
        /// The measure index of the host instrument ribbon. 
        /// This value is equal to the measure index of the host score measure.
        /// </summary>
        int MeasureIndex { get; }
        /// <summary>
        /// The index of the instrument ribbon in the score.
        /// </summary>
        int RibbonIndex { get; }

        /// <summary>
        /// Edit the blocks in the measure.
        /// </summary>
        /// <param name="voice"></param>
        /// <returns></returns>
        IEnumerable<IMeasureBlockEditor> EditBlocks(int voice);


        /// <summary>
        /// Clears the content of the instrument measure.
        /// </summary>
        void Clear();
        /// <summary>
        /// Clears the content of one voice in the measure.
        /// </summary>
        /// <param name="voice"></param>
        void ClearVoice(int voice);
        /// <summary>
        /// Adds a voice to the measure. Does nothing if the voice already exists.
        /// </summary>
        /// <param name="voice"></param>
        void AddVoice(int voice);


        /// <summary>
        /// Prepend a block to the measure blocks.
        /// </summary>
        /// <param name="voice"></param>
        /// <param name="duration"></param>
        /// <param name="grace"></param>
        void PrependBlock(int voice, Duration duration, bool grace);
        /// <summary>
        /// Appends a measure block after the blocks.
        /// </summary>
        /// <param name="voice"></param>
        /// <param name="duration"></param>
        /// <param name="grace"></param>
        void AppendBlock(int voice, Duration duration, bool grace);


        /// <summary>
        /// Applies the layout to the instrument measure.
        /// </summary>
        /// <param name="layout"></param>
        void ApplyLayout(IInstrumentMeasureLayout layout);
        /// <summary>
        /// Read the layout from the instrument measure.
        /// </summary>
        /// <returns></returns>
        IInstrumentMeasureLayout ReadLayout();
    }
}
