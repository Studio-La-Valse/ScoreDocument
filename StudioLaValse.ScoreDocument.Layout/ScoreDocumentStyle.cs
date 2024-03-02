using StudioLaValse.ScoreDocument.Core.Primitives;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// Represents the style for a score document.
    /// </summary>
    public class ScoreDocumentStyle
    {
        /// <summary>
        /// Create a default score document layout.
        /// </summary>
        public Func<IScoreDocument, ScoreDocumentLayout> ScoreDocumentLayoutFactory { get; set; } = (s) => new ScoreDocumentLayout("");

        /// <summary>
        /// Create a default note layout.
        /// </summary>
        public Func<INote, NoteLayout> NoteLayoutFactory { get; set; } = (n) => new NoteLayout();
        /// <summary>
        /// Create a default chord layout.
        /// </summary>
        public Func<IChord, ChordLayout> ChordLayoutFactory { get; set; } = (c) => new ChordLayout();
        /// <summary>
        /// Create a default measure block layout.
        /// </summary>
        public Func<IMeasureBlock, MeasureBlockLayout> MeasureBlockStyle { get; set; } = (m) => new MeasureBlockLayout();

        /// <summary>
        /// Create a default instrument measure layout.
        /// </summary>
        public Func<IInstrumentMeasure, InstrumentMeasureLayout> InstrumentMeasureLayoutFactory { get; set; } = (m) => new InstrumentMeasureLayout();
        /// <summary>
        /// Create a default score measure layout.
        /// </summary>
        public Func<IScoreMeasure, ScoreMeasureLayout> ScoreMeasureLayoutFactory { get; set; } = (m) => new ScoreMeasureLayout();
        /// <summary>
        /// Create a default instrument ribbon layout.
        /// </summary>
        public Func<IInstrumentRibbon, InstrumentRibbonLayout> InstrumentRibbonLayoutFactory { get; set; } = (i) => new InstrumentRibbonLayout(i.Instrument);


        /// <summary>
        /// Create a default staff layout.
        /// </summary>
        public Func<IStaff, StaffLayout> StaffLayoutFactory { get; set; } = (s) => new StaffLayout();
        /// <summary>
        /// Create a default staffgroup layout.
        /// </summary>
        public Func<IStaffGroup, StaffGroupLayout> StaffGroupLayoutFactory { get; set; } = (s) => new StaffGroupLayout(s.Instrument);
        /// <summary>
        /// Create a default staff system layout.
        /// </summary>
        public Func<IStaffSystem, StaffSystemLayout> StaffSystemLayoutFactory { get; set; } = (s) => new StaffSystemLayout();


        private ScoreDocumentStyle()
        {

        }
        /// <summary>
        /// Create a document style from the specified action.
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static ScoreDocumentStyle Create(Action<ScoreDocumentStyle> action)
        {
            var style = new ScoreDocumentStyle();
            action(style);
            return style;
        }
        /// <summary>
        /// Create a new default score document style.
        /// </summary>
        /// <returns></returns>
        public static ScoreDocumentStyle Create()
        {
            var style = new ScoreDocumentStyle();
            return style;
        }
    }
}
