namespace StudioLaValse.ScoreDocument.Implementation
{
    public sealed class GraceGroupLayout : IGraceGroupLayout
    {
        public bool OccupySpace { get; set; } = false;
        public double ChordSpacing { get; set; } = 5;
        public RythmicDuration ChordDuration { get; set; } = new(8);
        public double Scale { get; set; } = 1;

        public double StemLength => throw new NotImplementedException();

        public double BeamAngle => throw new NotImplementedException();

        public double BeamThickness => throw new NotImplementedException();

        public double BeamSpacing => throw new NotImplementedException();
    }
}
