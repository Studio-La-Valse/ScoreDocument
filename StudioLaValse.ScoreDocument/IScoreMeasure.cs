using StudioLaValse.ScoreDocument.Layout;
using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents a score measure editor.
    /// </summary>
    public interface IScoreMeasure : IScoreMeasureLayout, IScoreElement, IUniqueScoreElement
    {
        /// <summary>
        /// Specifies the index in the score.
        /// </summary>
        int IndexInScore { get; }

        /// <summary>
        /// Indicates if this measure is the last in the score.
        /// </summary>
        bool IsLastInScore { get; }

        /// <summary>
        /// The time signature of the measure.
        /// </summary>
        TimeSignature TimeSignature { get; }

        /// <summary>
        /// Enumerates the instrument measures of the score.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IInstrumentMeasure> ReadMeasures();

        /// <summary>
        /// Edit the instrument measure at the specified ribbon index.
        /// </summary>
        /// <param name="ribbonIndex"></param>
        /// <returns></returns>
        IInstrumentMeasure ReadMeasure(int ribbonIndex);

        /// <summary>
        /// Tries to read the previous score measure.
        /// </summary>
        /// <param name="previous"></param>
        /// <returns></returns>
        bool TryReadPrevious([NotNullWhen(true)] out IScoreMeasure? previous);

        /// <summary>
        /// Tries to read the next score measure.
        /// </summary>
        /// <param name="next"></param>
        /// <returns></returns>
        bool TryReadNext([NotNullWhen(true)] out IScoreMeasure? next);
    }
}
