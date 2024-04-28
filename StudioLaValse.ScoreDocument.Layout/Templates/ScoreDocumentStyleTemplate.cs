using StudioLaValse.ScoreDocument.Core;

namespace StudioLaValse.ScoreDocument.Layout.Templates
{
    public class ScoreDocumentStyleTemplate
    {
        public double Scale { get; set; } = 1.0;

        public double HorizontalStaffLineThickness { get; set; } = 0.075;

        public double VerticalStaffLineThickness { get; set; } = 0.25;

        public double StemLineThickness { get; set; } = 0.1;

        public double FirstSystemIndent { get; set; } = 15;

        public ColorARGB PageColor { get; set; } = new ColorARGB() { A = 255, R = 255, G = 255, B = 255 };  

        public ColorARGB ForegroundColor { get; set; } = new ColorARGB() { A = 255, R = 0, G = 0, B = 0 };

        public Dictionary<Instrument, double> InstrumentScales { get; set; } = new(new InstrumentComparer());

        public PageStyleTemplate PageStyleTemplate { get; init; } = new();

        public ScoreMeasureStyleTemplate ScoreMeasureStyleTemplate { get; init; } = new();

        public InstrumentMeasureStyleTemplate InstrumentMeasureStyleTemplate { get; init; } = new();

        public InstrumentRibbonStyleTemplate InstrumentRibbonStyleTemplate { get; init; } = new();

        public ChordStyleTemplate ChordStyleTemplate { get; init; } = new();

        public NoteStyleTemplate NoteStyleTemplate { get; init; } = new();

        public MeasureBlockStyleTemplate MeasureBlockStyleTemplate { get; init; } = new();

        public StaffStyleTemplate StaffStyleTemplate { get; init; } = new();

        public StaffGroupStyleTemplate StaffGroupStyleTemplate { get; init; } = new();

        public StaffSystemStyleTemplate StaffSystemStyleTemplate { get; init; } = new();

        public ScoreDocumentStyleTemplate()
        {

        }

        public void Apply(ScoreDocumentStyleTemplate styleTemplate)
        {
            Scale = styleTemplate.Scale;
            HorizontalStaffLineThickness = styleTemplate.HorizontalStaffLineThickness;
            VerticalStaffLineThickness = styleTemplate.VerticalStaffLineThickness;
            StemLineThickness = styleTemplate.StemLineThickness;
            FirstSystemIndent = styleTemplate.FirstSystemIndent;

            InstrumentScales.Clear();
            foreach (var kv in styleTemplate.InstrumentScales)
            {
                InstrumentScales.Add(kv.Key, kv.Value);
            }

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

            StaffGroupStyleTemplate.DistanceToNext = styleTemplate.StaffGroupStyleTemplate.DistanceToNext;

            StaffSystemStyleTemplate.PaddingBottom = styleTemplate.StaffSystemStyleTemplate.PaddingBottom;
        }
    }
}
