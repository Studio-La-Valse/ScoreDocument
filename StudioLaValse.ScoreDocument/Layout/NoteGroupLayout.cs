namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// The layout of a note group.
    /// </summary>
    public class NoteGroupLayout : INoteGroupLayout
    {
        /// <inheritdoc/>
        public double StemLength { get; }
        /// <inheritdoc/>
        public double BeamAngle { get; }

        /// <summary>
        /// Create the default layout.
        /// </summary>
        public NoteGroupLayout()
        {
            StemLength = 4;
            BeamAngle = 0;
        }

        /// <summary>
        /// Create a new layout.
        /// </summary>
        /// <param name="stemLength"></param>
        /// <param name="beamAngle"></param>
        public NoteGroupLayout(double stemLength, double beamAngle)
        {
            StemLength = stemLength;
            BeamAngle = beamAngle;
        }

        /// <inheritdoc/>
        public INoteGroupLayout Copy()
        {
            return new NoteGroupLayout(StemLength, BeamAngle);
        }
    }
}
