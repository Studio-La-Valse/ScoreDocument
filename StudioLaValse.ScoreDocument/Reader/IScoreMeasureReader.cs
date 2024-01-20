using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument.Reader
{
    /// <summary>
    /// Represents a score measure reader.
    /// </summary>
    public interface IScoreMeasureReader : IScoreMeasure, IUniqueScoreElement
    {
        /// <summary>
        /// Reads the index in the score.
        /// </summary>
        int IndexInScore { get; }
        /// <summary>
        /// Specifies whether the score measure is the last in the score.
        /// </summary>
        bool IsLastInScore { get; }

        /// <summary>
        /// Reads the instrument measures in the score measure.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IInstrumentMeasureReader> ReadMeasures();
        /// <summary>
        /// Reads the instrument measure at the specified ribbon index.
        /// </summary>
        /// <param name="ribbonIndex"></param>
        /// <returns></returns>
        IInstrumentMeasureReader ReadMeasure(int ribbonIndex);

        /// <summary>
        /// Tries to read the previous score measure.
        /// </summary>
        /// <param name="previous"></param>
        /// <returns></returns>
        bool TryReadPrevious([NotNullWhen(true)] out IScoreMeasureReader? previous);
        /// <summary>
        /// Tries to read the next score measure.
        /// </summary>
        /// <param name="next"></param>
        /// <returns></returns>
        bool TryReadNext([NotNullWhen(true)] out IScoreMeasureReader? next);

        /// <summary>
        /// Reads the layout of the score measure.
        /// </summary>
        /// <returns></returns>
        IScoreMeasureLayout ReadLayout();
        /// <summary>
        /// Reads the staff system origin.
        /// </summary>
        /// <returns></returns>
        IStaffSystemReader ReadStaffSystemOrigin();
    }
}
