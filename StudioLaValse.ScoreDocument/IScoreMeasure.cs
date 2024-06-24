using StudioLaValse.ScoreDocument.Layout;
using System.Diagnostics.CodeAnalysis;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents a score measure editor.
    /// </summary>
    public interface IScoreMeasure : IHasLayout<IScoreMeasureLayout>, IUniqueScoreElement, IScoreElement
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

        /// <summary>
        /// Assign the specified key signature to the score measure.
        /// </summary>
        /// <param name="keySignature"></param>
        void SetKeySignature(KeySignature keySignature);
        /// <summary>
        /// Applies inner padding to the left side of the measure. Absolute value that does not change with scaling.
        /// </summary>
        /// <param name="padding"></param>
        void SetPaddingLeft(double padding);
        /// <summary>
        /// Applies inner padding to the right side of the measure. Absolute value that does not change with scaling.
        /// </summary>
        /// <param name="padding"></param>
        void SetPaddingRight(double padding);
        /// <summary>
        /// Requests padding bottom for this measure in the staff system. 
        /// If no value is applied, no padding is requested. 
        /// The highest non-null value of all measures in the staff system will determine the padding of the staff system.
        /// If no non-null values in the staff system are found, the staff system style template will determine the padding below the staff system.
        /// </summary>
        /// <param name="padding"></param>
        void SetPaddingBottom(double? padding);
    }
}
