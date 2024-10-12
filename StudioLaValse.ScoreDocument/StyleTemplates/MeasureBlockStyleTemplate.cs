namespace StudioLaValse.ScoreDocument.StyleTemplates
{
    /// <summary>
    /// A measure block style template.
    /// </summary>
    public class MeasureBlockStyleTemplate
    {
        /// <summary>
        /// The global stem length of measure blocks.
        /// </summary>
        public required double StemLength { get; set; }
        /// <summary>
        /// The global stem thickness of measure blocks.
        /// </summary>
        public required double StemThickness { get; set; }
        /// <summary>
        /// The angle of beams (in degrees).
        /// </summary>
        public required double BeamAngle { get; set; }
        /// <summary>
        /// The global thickness of beams.
        /// </summary>
        public required double BeamThickness { get; set; }
        /// <summary>
        /// The global spacing between beams.
        /// </summary>
        public required double BeamSpacing { get; set; }

        /// <summary>
        /// The scale of all measure blocks.
        /// </summary>
        public required double Scale { get; set; }

        /// <summary>
        /// Create a default measure block style template.
        /// </summary>
        /// <returns></returns>
        public static MeasureBlockStyleTemplate Create()
        {
            return new MeasureBlockStyleTemplate()
            {
                StemLength = 5,
                BeamAngle = 0,
                BeamThickness = 0.6,
                BeamSpacing = 0.3,
                StemThickness = 0.1,
                Scale = 1
            };
        }

        /// <summary>
        /// Apply another measure block style template.
        /// </summary>
        /// <param name="styleTemplate"></param>
        public void Apply(MeasureBlockStyleTemplate styleTemplate)
        {
            StemLength = styleTemplate.StemLength;
            BeamAngle = styleTemplate.BeamAngle;
            BeamThickness = styleTemplate.BeamThickness;
            BeamSpacing = styleTemplate.BeamSpacing;
            StemThickness = styleTemplate.StemThickness;
            Scale = styleTemplate.StemThickness;
        }
    }
}
