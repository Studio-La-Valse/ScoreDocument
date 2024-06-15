namespace StudioLaValse.ScoreDocument.Layout.Templates
{
    public class ScoreMeasureStyleTemplate
    {
        public required double PaddingLeft { get; set; }
        public required double PaddingRight { get; set; }

        public static ScoreMeasureStyleTemplate Create()
        {
            return new ScoreMeasureStyleTemplate()
            {
                PaddingLeft = 5,
                PaddingRight = 0,
            };
        }

        public void Apply(ScoreMeasureStyleTemplate styleTemplate)
        {
            PaddingLeft = styleTemplate.PaddingLeft;
            PaddingRight = styleTemplate.PaddingRight;
        }
    }
}
