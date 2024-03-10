using StudioLaValse.ScoreDocument.Core.Primitives;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// Represents a source for the layout of a score document.
    /// </summary>
    public interface IScoreLayoutProvider
    {
        /// <summary>
        /// Get the layout for the specified element.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        ScoreDocumentLayout DocumentLayout<TElement>(TElement element) where TElement : IScoreDocument, IScoreEntity;

        /// <summary>
        /// Get the layout for the specified element.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        PageLayout PageLayout<TElement>(TElement element) where TElement : IPage, IScoreEntity;

        /// <summary>
        /// Get the layout for the specified element.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        ChordLayout ChordLayout<TElement>(TElement element) where TElement : IChord, IScoreEntity;
        /// <summary>
        /// Get the layout for the specified element.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        MeasureBlockLayout MeasureBlockLayout<TElement>(TElement element) where TElement : IMeasureBlock, IScoreEntity;
        /// <summary>
        /// Get the layout for the specified element.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        NoteLayout NoteLayout<TElement>(TElement element) where TElement : INote, IScoreEntity;


        /// <summary>
        /// Get the layout for the specified element.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        InstrumentRibbonLayout InstrumentRibbonLayout<TElement>(TElement element) where TElement : IInstrumentRibbon, IScoreEntity;
        /// <summary>
        /// Get the layout for the specified element.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        ScoreMeasureLayout ScoreMeasureLayout<TElement>(TElement element) where TElement : IScoreMeasure, IScoreEntity;
        /// <summary>
        /// Get the layout for the specified element.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        InstrumentMeasureLayout InstrumentMeasureLayout<TElement>(TElement element) where TElement : IInstrumentMeasure, IScoreEntity;


        /// <summary>
        /// Get the layout for the specified element.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        StaffLayout StaffLayout<TElement>(TElement element) where TElement : IStaff, IScoreEntity;
        /// <summary>
        /// Get the layout for the specified element.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        StaffGroupLayout StaffGroupLayout<TElement>(TElement element) where TElement : IStaffGroup, IScoreEntity;
        /// <summary>
        /// Get the layout for the specified element.
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        StaffSystemLayout StaffSystemLayout<TElement>(TElement element) where TElement : IStaffSystem, IScoreEntity;
    }
}