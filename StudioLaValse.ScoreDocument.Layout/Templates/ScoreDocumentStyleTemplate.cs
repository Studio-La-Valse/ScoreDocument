namespace StudioLaValse.ScoreDocument.Layout.Templates
{
    public class ScoreDocumentStyleTemplate
    {
        public PageStyleTemplate PageStyleTemplate { get; init; } = new();

        public ScoreMeasureStyleTemplate ScoreMeasureStyleTemplate { get; init; } = new();

        public InstrumentMeasureStyleTemplate InstrumentMeasureStyleTemplate { get; init; } = new();

        public InstrumentRibbonStyleTemplate InstrumentRibbonStyleTemplate { get; init; } = new();

        public ChordStyleTemplate ChordStyleTemplate { get; init; } = new();

        public NoteStyleTemplate NoteStyleTemplate { get; init; } = new ();

        public MeasureBlockStyleTemplate MeasureBlockStyleTemplate { get; init; } = new();   

        public StaffStyleTemplate StaffStyleTemplate { get; init; } = new();

        public StaffGroupStyleTemplate StaffGroupStyleTemplate { get; init; } = new();

        public StaffSystemStyleTemplate StaffSystemStyleTemplate { get; init; } = new();

        public ScoreDocumentStyleTemplate()
        {
            
        }

        public void Apply(ScoreDocumentStyleTemplate styleTemplate)
        {
            PageStyleTemplate.MarginLeft = styleTemplate.PageStyleTemplate.MarginLeft;
            PageStyleTemplate.MarginTop = styleTemplate.PageStyleTemplate.MarginTop;
            PageStyleTemplate.MarginBottom = styleTemplate.PageStyleTemplate.MarginBottom;
            PageStyleTemplate.MarginRight = styleTemplate.PageStyleTemplate.MarginRight;
            PageStyleTemplate.PageWidth = styleTemplate.PageStyleTemplate.PageWidth;
            PageStyleTemplate.PageHeight = styleTemplate.PageStyleTemplate.PageHeight;

            ScoreMeasureStyleTemplate.PaddingLeft = styleTemplate.ScoreMeasureStyleTemplate.PaddingLeft;
            ScoreMeasureStyleTemplate.PaddingRight = styleTemplate.ScoreMeasureStyleTemplate.PaddingRight;
            ScoreMeasureStyleTemplate.Width = styleTemplate.ScoreMeasureStyleTemplate.Width;

            NoteStyleTemplate.Scale = styleTemplate.NoteStyleTemplate.Scale;
            NoteStyleTemplate.AccidentalDisplay = styleTemplate.NoteStyleTemplate.AccidentalDisplay;

            MeasureBlockStyleTemplate.StemLength = styleTemplate.MeasureBlockStyleTemplate.StemLength;
            MeasureBlockStyleTemplate.BracketAngle = styleTemplate.MeasureBlockStyleTemplate.BracketAngle;

            StaffStyleTemplate.DistanceToNext = styleTemplate.StaffStyleTemplate.DistanceToNext;

            StaffGroupStyleTemplate.LineSpacing = styleTemplate.StaffGroupStyleTemplate.LineSpacing;

            StaffSystemStyleTemplate.PaddingBottom = styleTemplate.StaffSystemStyleTemplate.PaddingBottom;
        }
    }
}
