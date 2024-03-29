﻿namespace StudioLaValse.ScoreDocument.Core.Primitives
{
    /// <summary>
    /// Represents a primitive instrument ribbon.
    /// </summary>
    public interface IInstrumentRibbon
    {
        /// <summary>
        /// The index of the isntrument ribbon in the score.
        /// </summary>
        int IndexInScore { get; }
        /// <summary>
        /// The instrument of the instrument ribbon.
        /// </summary>
        Instrument Instrument { get; }
    }

    /// <inheritdoc/>
    public interface IInstrumentRibbon<TMeasure> : IInstrumentRibbon where TMeasure : IInstrumentMeasure
    {
        /// <summary>
        /// Enumerate the measures in the ribbon.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TMeasure> ReadMeasures();
        /// <summary>
        /// Edit the instrument measure at the specified index.
        /// </summary>
        /// <param name="measureIndex"></param>
        /// <returns></returns>
        TMeasure ReadMeasure(int measureIndex);
    }
}
