using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// Represents a score measure primitive.
    /// </summary>
    public interface IScoreMeasure<TMeasure, TSelf> where TSelf : IScoreMeasure<TMeasure, TSelf>
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
