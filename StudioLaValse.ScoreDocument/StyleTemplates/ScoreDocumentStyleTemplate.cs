﻿namespace StudioLaValse.ScoreDocument.StyleTemplates
{
    /// <summary>
    /// A score document style template.
    /// </summary>
    public class ScoreDocumentStyleTemplate
    {
        /// <summary>
        /// The global scale of the score.
        /// </summary>
        public required double Scale { get; set; }

        /// <summary>
        /// The line thickness of staves.
        /// </summary>
        public required double HorizontalStaffLineThickness { get; set; }

        /// <summary>
        /// The line thickness of staff systems.
        /// </summary>
        public required double VerticalStaffLineThickness { get; set; }

        /// <summary>
        /// The thickness of stems.
        /// </summary>
        public required double StemLineThickness { get; set; }

        /// <summary>
        /// The first system indent.
        /// </summary>
        public required double FirstSystemIndent { get; set; }

        /// <summary>
        /// The global page style template.
        /// </summary>
        public required PageStyleTemplate PageStyleTemplate { get; init; }

        /// <summary>
        /// The global score measure style template.
        /// </summary>
        public required ScoreMeasureStyleTemplate ScoreMeasureStyleTemplate { get; init; }

        /// <summary>
        /// The global instrument measure style template.
        /// </summary>
        public required InstrumentMeasureStyleTemplate InstrumentMeasureStyleTemplate { get; init; }

        /// <summary>
        /// The global instrument ribbon style template.
        /// </summary>
        public required InstrumentRibbonStyleTemplate InstrumentRibbonStyleTemplate { get; init; }

        /// <summary>
        /// The global chord style template.
        /// </summary>
        public required ChordStyleTemplate ChordStyleTemplate { get; init; }

        /// <summary>
        /// The global note style template.
        /// </summary>
        public required NoteStyleTemplate NoteStyleTemplate { get; init; }

        /// <summary>
        /// The global measure block style template.
        /// </summary>
        public required MeasureBlockStyleTemplate MeasureBlockStyleTemplate { get; init; }

        /// <summary>
        /// The global grace group style template.
        /// </summary>
        public required GraceGroupStyleTemplate GraceGroupStyleTemplate { get; init; }

        /// <summary>
        /// The global staff style template.
        /// </summary>
        public required StaffStyleTemplate StaffStyleTemplate { get; init; }

        /// <summary>
        /// The global staff group style template.
        /// </summary>
        public required StaffGroupStyleTemplate StaffGroupStyleTemplate { get; init; }

        /// <summary>
        /// The global staff system style template.
        /// </summary>
        public required StaffSystemStyleTemplate StaffSystemStyleTemplate { get; init; }



        /// <summary>
        /// Create the default score document style template.
        /// </summary>
        /// <returns></returns>
        public static ScoreDocumentStyleTemplate Create()
        {
            return new ScoreDocumentStyleTemplate()
            {
                Scale = 1.0,
                HorizontalStaffLineThickness = 0.075,
                VerticalStaffLineThickness = 0.25,
                FirstSystemIndent = 10,
                StemLineThickness = 0.1,
                PageStyleTemplate = PageStyleTemplate.Create(),
                ScoreMeasureStyleTemplate = ScoreMeasureStyleTemplate.Create(),
                InstrumentMeasureStyleTemplate = new(),
                InstrumentRibbonStyleTemplate = new(),
                ChordStyleTemplate = ChordStyleTemplate.Create(),
                NoteStyleTemplate = NoteStyleTemplate.Create(),
                MeasureBlockStyleTemplate = MeasureBlockStyleTemplate.Create(),
                GraceGroupStyleTemplate = GraceGroupStyleTemplate.Create(),
                StaffStyleTemplate = StaffStyleTemplate.Create(),
                StaffGroupStyleTemplate = StaffGroupStyleTemplate.Create(),
                StaffSystemStyleTemplate = StaffSystemStyleTemplate.Create()
            };
        }

        /// <summary>
        /// Apply another style template to this style template.
        /// </summary>
        /// <param name="styleTemplate"></param>
        public void Apply(ScoreDocumentStyleTemplate styleTemplate)
        {
            Scale = styleTemplate.Scale;
            HorizontalStaffLineThickness = styleTemplate.HorizontalStaffLineThickness;
            VerticalStaffLineThickness = styleTemplate.VerticalStaffLineThickness;
            StemLineThickness = styleTemplate.StemLineThickness;
            FirstSystemIndent = styleTemplate.FirstSystemIndent;

            PageStyleTemplate.Apply(styleTemplate.PageStyleTemplate);

            ScoreMeasureStyleTemplate.Apply(styleTemplate.ScoreMeasureStyleTemplate);

            MeasureBlockStyleTemplate.Apply(styleTemplate.MeasureBlockStyleTemplate);
            GraceGroupStyleTemplate.Apply(styleTemplate.GraceGroupStyleTemplate);
            ChordStyleTemplate.Apply(styleTemplate.ChordStyleTemplate);
            NoteStyleTemplate.Apply(styleTemplate.NoteStyleTemplate);

            StaffStyleTemplate.Apply(styleTemplate.StaffStyleTemplate);
            StaffGroupStyleTemplate.Apply(styleTemplate.StaffGroupStyleTemplate);
            StaffSystemStyleTemplate.Apply(styleTemplate.StaffSystemStyleTemplate);
        }
    }
}
