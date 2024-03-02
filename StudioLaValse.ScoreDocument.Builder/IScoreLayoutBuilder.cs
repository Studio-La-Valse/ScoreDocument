using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument.Builder
{
    /// <inheritdoc/>
    public interface IScoreLayoutBuilder : IScoreLayoutDictionary
    {
        /// <summary>
        /// Apply the layout to the specified element.
        /// </summary>
        /// <param name="chord"></param>
        /// <param name="layout"></param>
        void Apply(IChordEditor chord, ChordLayout layout);
        /// <summary>
        /// Removes any applied layout from this document.
        /// </summary>
        /// <param name="element"></param>
        void Restore(IChordEditor element);


        /// <summary>
        /// Apply the layout to the specified element.
        /// </summary>
        /// <param name="instrumentMeasure"></param>
        /// <param name="layout"></param>
        void Apply(IInstrumentMeasureEditor instrumentMeasure, InstrumentMeasureLayout layout);
        /// <summary>
        /// Removes any applied layout from this document.
        /// </summary>
        /// <param name="element"></param>
        void Restore(IInstrumentMeasureEditor element);


        /// <summary>
        /// Apply the layout to the specified element.
        /// </summary>
        /// <param name="instrumentRibbon"></param>
        /// <param name="layout"></param>
        void Apply(IInstrumentRibbonEditor instrumentRibbon, InstrumentRibbonLayout layout);
        /// <summary>
        /// Removes any applied layout from this document.
        /// </summary>
        /// <param name="element"></param>
        void Restore(IInstrumentRibbonEditor element);


        /// <summary>
        /// Apply the layout to the specified element.
        /// </summary>
        /// <param name="measureBlock"></param>
        /// <param name="layout"></param>
        void Apply(IMeasureBlockEditor measureBlock, MeasureBlockLayout layout);
        /// <summary>
        /// Removes any applied layout from this document.
        /// </summary>
        /// <param name="element"></param>
        void Restore(IMeasureBlockEditor element);


        /// <summary>
        /// Apply the layout to the specified element.
        /// </summary>
        /// <param name="note"></param>
        /// <param name="layout"></param>
        void Apply(INoteEditor note, NoteLayout layout);
        /// <summary>
        /// Removes any applied layout from this document.
        /// </summary>
        /// <param name="element"></param>
        void Restore(INoteEditor element);


        /// <summary>
        /// Apply the layout to the specified element.
        /// </summary>
        /// <param name="scoreMeasure"></param>
        /// <param name="layout"></param>
        void Apply(IScoreMeasureEditor scoreMeasure, ScoreMeasureLayout layout);
        /// <summary>
        /// Removes any applied layout from this document.
        /// </summary>
        /// <param name="element"></param>
        void Restore(IScoreMeasureEditor element);


        /// <summary>
        /// Apply the layout to the specified element.
        /// </summary>
        /// <param name="staff"></param>
        /// <param name="layout"></param>
        void Apply(IStaffEditor staff, StaffLayout layout);
        /// <summary>
        /// Removes any applied layout from this document.
        /// </summary>
        /// <param name="element"></param>
        void Restore(IStaffEditor element);


        /// <summary>
        /// Apply the layout to the specified element.
        /// </summary>
        /// <param name="staffGroup"></param>
        /// <param name="layout"></param>
        void Apply(IStaffGroupEditor staffGroup, StaffGroupLayout layout);
        /// <summary>
        /// Removes any applied layout from this document.
        /// </summary>
        /// <param name="element"></param>
        void Restore(IStaffGroupEditor element);


        /// <summary>
        /// Apply the layout to the specified element.
        /// </summary>
        /// <param name="staffSystem"></param>
        /// <param name="layout"></param>
        void Apply(IStaffSystemEditor staffSystem, StaffSystemLayout layout);
        /// <summary>
        /// Removes any applied layout from this document.
        /// </summary>
        /// <param name="element"></param>
        void Restore(IStaffSystemEditor element);


        /// <summary>
        /// Apply the layout to the specified element.
        /// </summary>
        /// <param name="layout"></param>
        void Apply(ScoreDocumentLayout layout);
        /// <summary>
        /// Removes the score document layout from this document.
        /// </summary>
        void Restore();


        /// <summary>
        /// Removes any applied layout from this document.
        /// </summary>
        void Clean();
    }
}
