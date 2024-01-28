using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Reader
{
    /// <summary>
    /// Represents an instrument measure reader.
    /// </summary>
    public interface IInstrumentMeasureReader : IInstrumentMeasure, IUniqueScoreElement
    {
        /// <summary>
        /// Reads the index in the score of the parent score measure.
        /// </summary>
        int MeasureIndex { get; }
        /// <summary>
        /// Reads the index in the score of the parent instrument ribbon.
        /// </summary>
        int RibbonIndex { get; }


        /// <summary>
        /// Reads the host score measure.
        /// </summary>
        /// <returns></returns>
        IScoreMeasureReader ReadMeasureContext();

        /// <summary>
        /// Reads the content of the measure.
        /// </summary>
        /// <param name="voice"></param>
        /// <returns></returns>
        IMeasureBlockChainReader ReadBlockChainAt(int voice);

        /// <summary>
        /// Tries to read the previous instrument measure.
        /// </summary>
        /// <param name="previous"></param>
        /// <returns><see langword="true"/> if it exists.</returns>
        bool TryReadPrevious([NotNullWhen(true)] out IInstrumentMeasureReader? previous);
        /// <summary>
        /// Tries to read the next instrument measure.
        /// </summary>
        /// <param name="next"></param>
        /// <returns><see langword="true"/> if it exists.</returns>
        bool TryReadNext([NotNullWhen(true)] out IInstrumentMeasureReader? next);

        /// <summary>
        /// Read the layout of the measure.
        /// </summary>
        /// <returns></returns>
        IInstrumentMeasureLayout ReadLayout();
    }
}
