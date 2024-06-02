namespace StudioLaValse.ScoreDocument.Layout.Templates
{
    public class MeasureBlockStyleTemplate
    {
        public required double StemLength { get; set; } = -5;
        public required double BeamAngle { get; set; } = 0;

        public static MeasureBlockStyleTemplate Create()
        {
            return new MeasureBlockStyleTemplate()
            {
                StemLength = -5,
                BeamAngle = 0
            };
        }
    }
}
