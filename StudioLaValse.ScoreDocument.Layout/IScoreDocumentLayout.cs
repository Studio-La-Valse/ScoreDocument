using StudioLaValse.ScoreDocument.Core.Primitives;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// Defines the base interface for a score document layout.
    /// </summary>
    public interface IScoreDocumentLayout
    {
        NoteLayout NoteLayout<TElement>(TElement element) where TElement : INote, IScoreEntity;
        ChordLayout ChordLayout<TElement>(TElement element) where TElement : IChord, IScoreEntity;
        MeasureBlockLayout MeasureBlockLayout<TElement>(TElement element) where TElement : IMeasureBlock, IScoreEntity;
        InstrumentMeasureLayout InstrumentMeasureLayout<TElement>(TElement element) where TElement : IInstrumentMeasure, IScoreEntity;
        ScoreMeasureLayout ScoreMeasureLayout<TElement>(TElement element) where TElement : IScoreMeasure, IScoreEntity;
        InstrumentRibbonLayout InstrumentRibbonLayout<TElement>(TElement element) where TElement : IInstrumentRibbon, IScoreEntity;
        StaffLayout StaffLayout<TElement>(TElement element) where TElement : IStaff, IScoreEntity;
        StaffGroupLayout StaffGroupLayout<TElement>(TElement element) where TElement : IStaffGroup, IScoreEntity;
        StaffSystemLayout StaffSystemLayout<TElement>(TElement element) where TElement : IStaffSystem, IScoreEntity;
        PageLayout PageLayout<TElement>(TElement element) where TElement : IPage, IScoreEntity;
        ScoreDocumentLayout DocumentLayout<TElement>(TElement element) where TElement : IScoreDocument, IScoreEntity;
    }
}