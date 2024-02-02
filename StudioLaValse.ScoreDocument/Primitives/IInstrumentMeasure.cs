namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// Represents a primitive instrument measure.
    /// </summary>
    public interface IInstrumentMeasure : IUniqueScoreElement
    {
        /// <summary>
        /// The instrument of the measure.
        /// </summary>
        Instrument Instrument { get; }
        /// <summary>
        /// The time signature of the measure.
        /// </summary>
        TimeSignature TimeSignature { get; }
        /// <summary>
        /// Enumemerate the voices in the measure.
        /// </summary>
        /// <returns></returns>
        IEnumerable<int> EnumerateVoices();
        /// <summary>
        /// Enumerate the measure blocks with the specified voice.
        /// </summary>
        /// <param name="voice"></param>
        /// <returns></returns>
        IMeasureBlockChain BlockChainAt(int voice);
    }
}
