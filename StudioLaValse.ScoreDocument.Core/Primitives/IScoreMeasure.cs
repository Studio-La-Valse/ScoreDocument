using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Core.Primitives
{
    /// <summary>
    /// Represents a score measure primitive.
    /// </summary>
    public interface IScoreMeasure
    {
        /// <summary>
        /// Specifies the index in the score.
        /// </summary>
        int IndexInScore { get; }
        /// <summary>
        /// The time signature of the measure.
        /// </summary>
        TimeSignature TimeSignature { get; }
        /// <summary>
        /// The key signature of the measure.
        /// </summary>
        KeySignature KeySignature { get; }
    }

    /// <inheritdoc/>
    public interface IScoreMeasure<TMeasure> : IScoreMeasure where TMeasure : IInstrumentMeasure
    {

        /// <summary>
        /// Enumerates the instrument measures of the score.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TMeasure> ReadMeasures();
        /// <summary>
        /// Edit the instrument measure at the specified ribbon index.
        /// </summary>
        /// <param name="ribbonIndex"></param>
        /// <returns></returns>
        TMeasure ReadMeasure(int ribbonIndex);
    }

    /// <inheritdoc/>
    public interface IScoreMeasure<TMeasure, TSelf> : IScoreMeasure<TMeasure> where TMeasure : IInstrumentMeasure
                                                                              where TSelf : IScoreMeasure
    {
        /// <summary>
        /// Tries to read the previous score measure.
        /// </summary>
        /// <param name="previous"></param>
        /// <returns></returns>
        bool TryReadPrevious([NotNullWhen(true)] out TSelf? previous);
        /// <summary>
        /// Tries to read the next score measure.
        /// </summary>
        /// <param name="next"></param>
        /// <returns></returns>
        bool TryReadNext([NotNullWhen(true)] out TSelf? next);
    }
}
