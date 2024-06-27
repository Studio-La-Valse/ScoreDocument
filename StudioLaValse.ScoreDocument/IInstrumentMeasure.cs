using StudioLaValse.ScoreDocument.Layout;
using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents an instrument measure editor.
    /// </summary>
    public interface IInstrumentMeasure : IInstrumentMeasureLayout, IScoreElement, IUniqueScoreElement
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
        IEnumerable<int> ReadVoices();

        /// <summary>
        /// Read the blockchain for the specified voice.
        /// </summary>
        /// <param name="voice"></param>
        /// <returns></returns>
        IMeasureBlockChain ReadBlockChainAt(int voice);

        /// <summary>
        /// Tries to read the previous instrument measure.
        /// </summary>
        /// <param name="previous"></param>
        /// <returns><see langword="true"/> if it exists.</returns>
        bool TryReadPrevious([NotNullWhen(true)] out IInstrumentMeasure? previous);

        /// <summary>
        /// Tries to read the next instrument measure.
        /// </summary>
        /// <param name="next"></param>
        /// <returns><see langword="true"/> if it exists.</returns>
        bool TryReadNext([NotNullWhen(true)] out IInstrumentMeasure? next);

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
    }
}
