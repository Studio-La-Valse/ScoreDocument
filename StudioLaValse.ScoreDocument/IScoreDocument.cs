using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Models;
using StudioLaValse.ScoreDocument.StyleTemplates;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents the score document editor.
    /// </summary>
    public interface IScoreDocument : IScoreDocumentLayout, IScoreElement, IUniqueScoreElement
    {
        /// <summary>
        /// The number of score measures in the score.
        /// </summary>
        int NumberOfMeasures { get; }

        /// <summary>
        /// The number of instrument ribbons in the score.
        /// </summary>
        int NumberOfInstruments { get; }

        /// <summary>
        /// Enumerates the score measures in the score.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IScoreMeasure> ReadScoreMeasures();

        /// <summary>
        /// Enumerates the instrument ribbons in the score.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IInstrumentRibbon> ReadInstrumentRibbons();
        /// <summary>
        /// Get the score measure at the specified index in the score.
        /// If there is no measure at the specified index, an exception will be thrown.
        /// </summary>
        /// <param name="indexInScore"></param>
        /// <returns></returns>
        IScoreMeasure ReadScoreMeasure(int indexInScore);

        /// <summary>
        /// Get the instrument ribbon at the specified index in the score.
        /// If no such element exists at the index, an exception is thrown.
        /// </summary>
        /// <param name="indexInScore"></param>
        /// <returns></returns>
        IInstrumentRibbon ReadInstrumentRibbon(int indexInScore);

        /// <summary>
        /// Clears all content of the score.
        /// </summary>
        void Clear();

        /// <summary>
        /// Add an instrument ribbon from the instrument at the end of the score.
        /// </summary>
        /// <param name="instrument"></param>
        void AddInstrumentRibbon(Instrument instrument);

        /// <summary>
        /// Remove an instrument ribbon with the specified element id.
        /// Throws an exception if no element with the specified element is found.
        /// </summary>
        /// <param name="indexInScore"></param>
        void RemoveInstrumentRibbon(int indexInScore);

        /// <summary>
        /// Append a score measure to the end of the score. 
        /// If no time signature is specified, the time signature of the previous measure is used.
        /// If there is no previous measure, and no time signature is specified, 4/4 will be used.
        /// </summary>
        /// <param name="timeSignature"></param>
        void AppendScoreMeasure(TimeSignature? timeSignature = null);

        /// <summary>
        /// Insert a score measure at the specified index.
        /// If no time signature is specified, the time signature of the previous measure is used.
        /// If there is no previous measure, and no time signature is specified, 4/4 will be used.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="timeSignature"></param>
        void InsertScoreMeasure(int index, TimeSignature? timeSignature = null);

        /// <summary>
        /// Remove the score measure at the specified index in the score.
        /// If there is no measure at the specified index, an exception will be thrown.
        /// </summary>
        /// <param name="indexInScore"></param>
        void RemoveScoreMeasure(int indexInScore);

        /// <summary>
        /// Edit the currently applied style template.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        void Edit(Action<ScoreDocumentStyleTemplate> action);

        /// <summary>
        /// Apply a new style template.
        /// </summary>
        /// <param name="scoreDocumentStyleTemplate"></param>
        /// <returns></returns>
        void Edit(ScoreDocumentStyleTemplate scoreDocumentStyleTemplate);

        /// <summary>
        /// Freeze the current score document into a serializable object.
        /// </summary>
        /// <returns></returns>
        ScoreDocumentModel Freeze();

        /// <summary>
        /// Freeze the current layout into a serializable object.
        /// Note that the layout values may be different from the layout values in the current score document,
        /// because an internal layout select may read layout values from the original author layout. 
        /// The frozen layout model always reflects user layout values.
        /// The model is trimmed, so that empty layout models are ommitted.
        /// </summary>
        /// <returns></returns>
        ScoreDocumentLayoutDictionary FreezeLayout();
    }
}