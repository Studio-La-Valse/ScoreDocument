using StudioLaValse.ScoreDocument.Core.Primitives;
using StudioLaValse.ScoreDocument.Layout.ScoreElements;

namespace StudioLaValse.ScoreDocument.Layout
{
    public class ScoreDocumentStyle
    {
        public Func<IScoreDocument, ScoreDocumentLayout> ScoreDocumentLayoutFactory { get; set; } = (s) => new ScoreDocumentLayout("");


        public Func<INote, NoteLayout> NoteLayoutFactory { get; set; } = (n) => new NoteLayout();
        public Func<IChord, ChordLayout> ChordLayoutFactory { get; set; } = (c) => new ChordLayout();
        public Func<IMeasureBlock, MeasureBlockLayout> MeasureBlockStyle { get; set; } = (m) => new MeasureBlockLayout();


        public Func<IInstrumentMeasure, InstrumentMeasureLayout> InstrumentMeasureLayoutFactory { get; set; } = (m) => new InstrumentMeasureLayout();
        public Func<IScoreMeasure, ScoreMeasureLayout> ScoreMeasureLayoutFactory { get; set; } = (m) => new ScoreMeasureLayout();
        public Func<IInstrumentRibbon, InstrumentRibbonLayout> InstrumentRibbonLayoutFactory { get; set; } = (i) => new InstrumentRibbonLayout(i.Instrument);

        public Func<IStaff, StaffLayout> StaffLayoutFactory { get; set; } = (s) => new StaffLayout();
        public Func<IStaffGroup, StaffGroupLayout> StaffGroupLayoutFactory { get; set; } = (s) => new StaffGroupLayout(s.Instrument);
        public Func<IStaffSystem, StaffSystemLayout> StaffSystemLayoutFactory { get; set; } = (s) => new StaffSystemLayout();


        private ScoreDocumentStyle()
        {

        }
        public static ScoreDocumentStyle Create(Action<ScoreDocumentStyle> action)
        {
            var style = new ScoreDocumentStyle();
            action(style);
            return style;
        }
        public static ScoreDocumentStyle Create()
        {
            var style = new ScoreDocumentStyle();
            return style;
        }
    }
}
