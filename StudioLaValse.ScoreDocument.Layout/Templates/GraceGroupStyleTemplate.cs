using StudioLaValse.ScoreDocument.Core;

namespace StudioLaValse.ScoreDocument.Layout.Templates
{
    public class GraceGroupStyleTemplate : MeasureBlockStyleTemplate
    {
        public required bool OccupySpace { get; set; }
        public required double ChordSpaceRight { get; set; }
        public required PowerOfTwo ChordDuration { get; set; }
        public required double Scale { get; set; }

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
                Scale = 0.5
            };
        }

        public void Apply(GraceGroupStyleTemplate styleTemplate)
        {
            StemLength = styleTemplate.StemLength;
            BeamAngle = styleTemplate.BeamAngle;
            BeamThickness = styleTemplate.BeamThickness;
            BeamSpacing = styleTemplate.BeamSpacing;
            OccupySpace = styleTemplate.OccupySpace;
            ChordSpaceRight = styleTemplate.ChordSpaceRight;
            Scale = styleTemplate.Scale;
        }
    }
}
