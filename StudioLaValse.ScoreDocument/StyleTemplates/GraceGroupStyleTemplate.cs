global using StudioLaValse.ScoreDocument.Core;

namespace StudioLaValse.ScoreDocument.StyleTemplates
{
    /// <summary>
    /// A grace group style template.
    /// </summary>
    public class GraceGroupStyleTemplate : MeasureBlockStyleTemplate
    {
        /// <summary>
        /// Specifies whether the grace group occupies space.
        /// If true, non grace elements will be spaced such that this grace group fits in between.
        /// If false, the positions of regular elements are note altered.
        /// </summary>
        public required bool OccupySpace { get; set; }
        /// <summary>
        /// Gets or sets the global chord spacing of all grace groups.
        /// </summary>
        public required double ChordSpaceRight { get; set; }
        /// <summary>
        /// Gets or sets the default chord duration of all grace groups.
        /// </summary>
        public required PowerOfTwo ChordDuration { get; set; }


        /// <summary>
        /// Creates a default grace group style template.
        /// </summary>
        /// <returns></returns>
        new public static GraceGroupStyleTemplate Create()
        {
            return new GraceGroupStyleTemplate()
            {
                StemLength = -5,
                BeamAngle = 0,
                BeamThickness = 0.8,
                BeamSpacing = 0.3,
                OccupySpace = true,
                ChordSpaceRight = 4,
                ChordDuration = 8,
                Scale = 0.5,
                StemThickness = 0.1
            };
        }

        /// <summary>
        /// Applies another style template to this style template.
        /// </summary>
        /// <param name="styleTemplate"></param>
        public void Apply(GraceGroupStyleTemplate styleTemplate)
        {
            base.Apply(styleTemplate);

            StemLength = styleTemplate.StemLength;
            BeamAngle = styleTemplate.BeamAngle;
            BeamThickness = styleTemplate.BeamThickness;
            BeamSpacing = styleTemplate.BeamSpacing;
            OccupySpace = styleTemplate.OccupySpace;
            ChordSpaceRight = styleTemplate.ChordSpaceRight;
        }
    }
}
