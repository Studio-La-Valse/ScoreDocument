namespace StudioLaValse.ScoreDocument.StyleTemplates
{
    /// <summary>
    /// A chord style template.
    /// </summary>
    public class ChordStyleTemplate
    {
        /// <summary>
        /// Gets or sets the space to the next position (in mm).
        /// </summary>
        public required double SpaceRight { get; set; }

        /// <summary>
        /// Create a default chord style template.
        /// </summary>
        /// <returns></returns>
        public static ChordStyleTemplate Create()
        {
            return new ChordStyleTemplate()
            {
                SpaceRight = 3
            };
        }

        /// <summary>
        /// Apply another chord style template.
        /// </summary>
        /// <param name="styleTemplate"></param>
        public void Apply(ChordStyleTemplate styleTemplate)
        {
            SpaceRight = styleTemplate.SpaceRight;
        }
    }
}
