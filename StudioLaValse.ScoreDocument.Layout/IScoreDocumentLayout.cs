using StudioLaValse.ScoreDocument.Core.Primitives;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// Defines the base interface for a score document layout.
    /// </summary>
    public interface IScoreDocumentLayout
    {
        ChordLayout ChordLayout<TElement>(TElement element) where TElement : IChord, IScoreEntity;
        ScoreDocumentLayout DocumentLayout<TElement>(TElement element) where TElement : IScoreDocument, IScoreEntity;
        InstrumentMeasureLayout InstrumentMeasureLayout<TElement>(TElement element) where TElement : IInstrumentMeasure, IScoreEntity;
        InstrumentRibbonLayout InstrumentRibbonLayout<TElement>(TElement element) where TElement : IInstrumentRibbon, IScoreEntity;
        MeasureBlockLayout MeasureBlockLayout<TElement>(TElement element) where TElement : IMeasureBlock, IScoreEntity;
        NoteLayout NoteLayout<TElement>(TElement element) where TElement : INote, IScoreEntity;
        PageLayout PageLayout<TElement>(TElement element) where TElement : IPage, IScoreEntity;
        ScoreMeasureLayout ScoreMeasureLayout<TElement>(TElement element) where TElement : IScoreMeasure, IScoreEntity;
        StaffGroupLayout StaffGroupLayout<TElement>(TElement element) where TElement : IStaffGroup, IScoreEntity;
        StaffLayout StaffLayout<TElement>(TElement element) where TElement : IStaff, IScoreEntity;
        StaffSystemLayout StaffSystemLayout<TElement>(TElement element) where TElement : IStaffSystem, IScoreEntity;
    }
}