namespace StudioLaValse.ScoreDocument.Layout
{
    public class NoteGroupLayout : INoteGroupLayout
    {
        public double StemLength { get; }
        public double BeamAngle { get; }

        public NoteGroupLayout()
        {
            StemLength = 4;
            BeamAngle = 0;
        }

        public NoteGroupLayout(double stemLength, double beamAngle)
        {
            StemLength = stemLength;
            BeamAngle = beamAngle;
        }

        public INoteGroupLayout Copy()
        {
            return new NoteGroupLayout(StemLength, BeamAngle); 
        }
    }
}
