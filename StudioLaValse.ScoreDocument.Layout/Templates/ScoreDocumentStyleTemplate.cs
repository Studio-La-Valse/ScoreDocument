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
                ChordPositionFactor = 0.5,
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

            PageStyleTemplate.Apply(styleTemplate.PageStyleTemplate);

            ScoreMeasureStyleTemplate.Apply(styleTemplate.ScoreMeasureStyleTemplate);

            MeasureBlockStyleTemplate.Apply(styleTemplate.MeasureBlockStyleTemplate);
            ChordStyleTemplate.Apply(styleTemplate.ChordStyleTemplate);
            NoteStyleTemplate.Apply(styleTemplate.NoteStyleTemplate);

            StaffStyleTemplate.Apply(styleTemplate.StaffStyleTemplate);
            StaffGroupStyleTemplate.Apply(styleTemplate.StaffGroupStyleTemplate);
            StaffSystemStyleTemplate.Apply(styleTemplate.StaffSystemStyleTemplate);
        }
    }
}
