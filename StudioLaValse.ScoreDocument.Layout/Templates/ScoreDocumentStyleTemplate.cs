using StudioLaValse.ScoreDocument.Core;

namespace StudioLaValse.ScoreDocument.Layout.Templates
{
    public class ScoreDocumentStyleTemplate
    {
        public required double Scale { get; set; }

        public required double HorizontalStaffLineThickness { get; set; }

        public required double VerticalStaffLineThickness { get; set; }

        public required double StemLineThickness { get; set; }

        public required double FirstSystemIndent { get; set; }

        public required double ChordPositionFactor { get; set; }

        public required Dictionary<Guid, double> InstrumentScales { get; init; }



        public required PageStyleTemplate PageStyleTemplate { get; init; }

        public required ScoreMeasureStyleTemplate ScoreMeasureStyleTemplate { get; init; }

        public required InstrumentMeasureStyleTemplate InstrumentMeasureStyleTemplate { get; init; }

        public required InstrumentRibbonStyleTemplate InstrumentRibbonStyleTemplate { get; init; }

        public required ChordStyleTemplate ChordStyleTemplate { get; init; } 

        public required NoteStyleTemplate NoteStyleTemplate { get; init; }

        public required MeasureBlockStyleTemplate MeasureBlockStyleTemplate { get; init; }

        public required StaffStyleTemplate StaffStyleTemplate { get; init; }

        public required StaffGroupStyleTemplate StaffGroupStyleTemplate { get; init; }

        public required StaffSystemStyleTemplate StaffSystemStyleTemplate { get; init; }




        public static ScoreDocumentStyleTemplate Create()
        {
            return new ScoreDocumentStyleTemplate()
            {
                Scale = 1,
                HorizontalStaffLineThickness = 0.075,
                VerticalStaffLineThickness = 0.25,
                FirstSystemIndent = 15,
                StemLineThickness = 0.1,
                ChordPositionFactor = 0,
                InstrumentScales = [],
                PageStyleTemplate = PageStyleTemplate.Create(),
                ScoreMeasureStyleTemplate = ScoreMeasureStyleTemplate.Create(),
                InstrumentMeasureStyleTemplate = new(),
                InstrumentRibbonStyleTemplate = new(),
                ChordStyleTemplate = ChordStyleTemplate.Create(),
                NoteStyleTemplate = NoteStyleTemplate.Create(),
                MeasureBlockStyleTemplate = MeasureBlockStyleTemplate.Create(),
                StaffStyleTemplate = StaffStyleTemplate.Create(),
                StaffGroupStyleTemplate = StaffGroupStyleTemplate.Create(),
                StaffSystemStyleTemplate = StaffSystemStyleTemplate.Create()
            };
        }

        public void Apply(ScoreDocumentStyleTemplate styleTemplate)
        {
            Scale = styleTemplate.Scale;
            HorizontalStaffLineThickness = styleTemplate.HorizontalStaffLineThickness;
            VerticalStaffLineThickness = styleTemplate.VerticalStaffLineThickness;
            StemLineThickness = styleTemplate.StemLineThickness;
            FirstSystemIndent = styleTemplate.FirstSystemIndent;
            ChordPositionFactor = styleTemplate.ChordPositionFactor;

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

            NoteStyleTemplate.Scale = styleTemplate.NoteStyleTemplate.Scale;
            NoteStyleTemplate.AccidentalDisplay = styleTemplate.NoteStyleTemplate.AccidentalDisplay;

            MeasureBlockStyleTemplate.StemLength = styleTemplate.MeasureBlockStyleTemplate.StemLength;
            MeasureBlockStyleTemplate.BeamAngle = styleTemplate.MeasureBlockStyleTemplate.BeamAngle;

            ChordStyleTemplate.SpaceRight = styleTemplate.ChordStyleTemplate.SpaceRight;

            StaffStyleTemplate.DistanceToNext = styleTemplate.StaffStyleTemplate.DistanceToNext;
            StaffGroupStyleTemplate.DistanceToNext = styleTemplate.StaffGroupStyleTemplate.DistanceToNext;
            StaffSystemStyleTemplate.DistanceToNext = styleTemplate.StaffSystemStyleTemplate.DistanceToNext;
        }
    }
}
