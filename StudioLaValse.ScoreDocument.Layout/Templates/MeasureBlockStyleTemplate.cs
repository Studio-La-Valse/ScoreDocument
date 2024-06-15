namespace StudioLaValse.ScoreDocument.Layout.Templates
{
    public class MeasureBlockStyleTemplate
    {
        public required double StemLength { get; set; }
        public required double BeamAngle { get; set; }
        public required double BeamThickness { get; set; }
        public required double BeamSpacing { get; set; }

        public static MeasureBlockStyleTemplate Create()
        {
            return new MeasureBlockStyleTemplate()
            {
                StemLength = 10,
                BeamAngle = 0,
                BeamThickness = 0.5,
                BeamSpacing = 0.2
            };
        }

        public void Apply(MeasureBlockStyleTemplate styleTemplate)
        {
            StemLength = styleTemplate.StemLength;
            BeamAngle = styleTemplate.BeamAngle;
            BeamThickness = styleTemplate.BeamThickness;
            BeamSpacing = styleTemplate.BeamSpacing;
        }
    }
}
