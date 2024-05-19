﻿namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// Represents a primitive score document.
    /// </summary>
    public interface IScoreDocument : IScoreElement, IScoreEntity
    {
        /// <summary>
        /// The number of score measures in the score.
        /// </summary>
        int NumberOfMeasures { get; }
        /// <summary>
        /// The number of instrument ribbons in the score.
        /// </summary>
        int NumberOfInstruments { get; }
    }

    /// <inheritdoc/>
    public interface IScoreDocument<TScoreMeasure, TRibbon> : IScoreDocument where TScoreMeasure : IScoreMeasure
                                                                             where TRibbon : IInstrumentRibbon
    {
        /// <summary>
        /// Enumerates the score measures in the score.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TScoreMeasure> ReadScoreMeasures();
        /// <summary>
        /// Enumerates the instrument ribbons in the score.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TRibbon> ReadInstrumentRibbons();
        /// <summary>
        /// Get the score measure at the specified index in the score.
        /// If there is no measure at the specified index, an exception will be thrown.
        /// </summary>
        /// <param name="indexInScore"></param>
        /// <returns></returns>
        TScoreMeasure ReadScoreMeasure(int indexInScore);
        /// <summary>
        /// Get the instrument ribbon at the specified index in the score.
        /// If no such element exists at the index, an exception is thrown.
        /// </summary>
        /// <param name="indexInScore"></param>
        /// <returns></returns>
        TRibbon ReadInstrumentRibbon(int indexInScore);
    }

    /// <inheritdoc/>
    public interface IScoreDocument<TPage, TScoreMeasure, TRibbon> : IScoreDocument<TScoreMeasure, TRibbon> where TPage : IPage where TScoreMeasure : IScoreMeasure where TRibbon : IInstrumentRibbon
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<TPage> GeneratePages();
    }
}