using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// Represents a primitive instrument measure.
    /// </summary>
    public interface IInstrumentMeasure
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
        /// The key signature of the measure.
        /// </summary>
        KeySignature KeySignature { get; }
        /// <summary>
        /// Enumemerate the voices in the measure.
        /// </summary>
        /// <returns></returns>
        IEnumerable<int> ReadVoices();
    }

    /// <inheritdoc/>
    public interface IInstrumentMeasure<TMeasureBlockChain> : IInstrumentMeasure where TMeasureBlockChain : IMeasureBlockChain
    {
        /// <inheritdoc/>
        TMeasureBlockChain ReadBlockChainAt(int voice);
    }

    /// <inheritdoc/>
    public interface IInstrumentMeasure<TMeasureBlockChain, TSelf> : IInstrumentMeasure<TMeasureBlockChain> where TMeasureBlockChain : IMeasureBlockChain
                                                                                                            where TSelf : IInstrumentMeasure
    {
        /// <summary>
        /// Tries to read the previous instrument measure.
        /// </summary>
        /// <param name="previous"></param>
        /// <returns><see langword="true"/> if it exists.</returns>
        bool TryReadPrevious([NotNullWhen(true)] out TSelf? previous);
        /// <summary>
        /// Tries to read the next instrument measure.
        /// </summary>
        /// <param name="next"></param>
        /// <returns><see langword="true"/> if it exists.</returns>
        bool TryReadNext([NotNullWhen(true)] out TSelf? next);
    }
}
