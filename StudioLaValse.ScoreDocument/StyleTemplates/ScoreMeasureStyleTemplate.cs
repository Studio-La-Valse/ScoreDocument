namespace StudioLaValse.ScoreDocument.StyleTemplates
{
    /// <summary>
    /// A score measure style template.
    /// </summary>
    public class ScoreMeasureStyleTemplate
    {
        /// <summary>
        /// The available space to the left of the inside of the measure, before content starts.
        /// </summary>
        public required double PaddingLeft { get; set; }
        /// <summary>
        /// The available space to the right of the inside of the measure, after the content ends.
        /// </summary>
        public required double PaddingRight { get; set; }

        /// <summary>
        /// Create a default style template.
        /// </summary>
        /// <returns></returns>
        public static ScoreMeasureStyleTemplate Create()
        {
            return new ScoreMeasureStyleTemplate()
            {
                PaddingLeft = 5,
                PaddingRight = 0,
            };
        }

        /// <summary>
        /// Apply another style template to this template.
        /// </summary>
        /// <param name="styleTemplate"></param>
        public void Apply(ScoreMeasureStyleTemplate styleTemplate)
        {
            PaddingLeft = styleTemplate.PaddingLeft;
            PaddingRight = styleTemplate.PaddingRight;
        }
    }
}
