using StudioLaValse.ScoreDocument.Core.Primitives;
using StudioLaValse.ScoreDocument.Layout.ScoreElements;

namespace StudioLaValse.ScoreDocument.Layout
{
    public interface IScoreLayoutDictionary
    {
        ScoreDocumentLayout GetOrDefault(IScoreDocument document);


        ChordLayout GetOrDefault(IChord chord);
        MeasureBlockLayout GetOrDefault(IMeasureBlock chord);
        NoteLayout GetOrDefault(INote note);


        InstrumentRibbonLayout GetOrDefault(IInstrumentRibbon instrumentRibbon);
        ScoreMeasureLayout GetOrDefault(IScoreMeasure scoreMeasure);
        InstrumentMeasureLayout GetOrDefault(IInstrumentMeasure instrumentMeasure);


        StaffLayout GetOrDefault(IStaff staff);
        StaffGroupLayout GetOrDefault(IStaffGroup staffGroup);
        StaffSystemLayout GetOrDefault(IStaffSystem staffSystem);


        IEnumerable<IStaffSystem> EnumerateStaffSystems(IScoreDocumentReader scoreDocument);
    }
}