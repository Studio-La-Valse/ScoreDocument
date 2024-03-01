using StudioLaValse.ScoreDocument.Core.Primitives;
using StudioLaValse.ScoreDocument.Layout;
using StudioLaValse.ScoreDocument.Layout.ScoreElements;

namespace StudioLaValse.ScoreDocument.Builder
{
    public interface IScoreLayoutDictionary
    {
        void Apply(IChord chord, ChordLayout layout);
        void Apply(IInstrumentMeasure instrumentMeasure, InstrumentMeasureLayout layout);
        void Apply(IInstrumentRibbon instrumentRibbon, InstrumentRibbonLayout layout);
        void Apply(IMeasureBlock measureBlock, MeasureBlockLayout layout);
        void Apply(INote note, NoteLayout layout);
        void Apply(IScoreDocument scoreDocument, ScoreDocumentLayout layout);
        void Apply(IScoreMeasure scoreMeasure, ScoreMeasureLayout layout);
        void Apply(IStaff staff, StaffLayout layout);
        void Apply(IStaffGroup staffGroup, StaffGroupLayout layout);
        void Apply(IStaffSystem staffSystem, StaffSystemLayout layout);
        ChordLayout GetOrDefault(IChord chord);
        InstrumentMeasureLayout GetOrDefault(IInstrumentMeasure instrumentMeasure);
        InstrumentRibbonLayout GetOrDefault(IInstrumentRibbon instrumentRibbon);
        MeasureBlockLayout GetOrDefault(IMeasureBlock chord);
        NoteLayout GetOrDefault(INote note);
        ScoreDocumentLayout GetOrDefault(IScoreDocument document);
        ScoreMeasureLayout GetOrDefault(IScoreMeasure scoreMeasure);
        StaffLayout GetOrDefault(IStaff staff);
        StaffGroupLayout GetOrDefault(IStaffGroup staffGroup);
        StaffSystemLayout GetOrDefault(IStaffSystem staffSystem);
    }
}
