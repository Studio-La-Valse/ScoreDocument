namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// The layout of a note group.
    /// </summary>
    public class MeasureBlockLayout : ILayoutElement<MeasureBlockLayout>
    {
        /// <inheritdoc/>
        public double StemLength { get; set; }
        /// <inheritdoc/>
        public double BeamAngle { get; set; }

        /// <summary>
        /// Create the default layout.
        /// </summary>
        public MeasureBlockLayout(double stemLength = 4, double beamAngle = 0)
        {
            StemLength = stemLength;
            BeamAngle = beamAngle;
        }


        /// <inheritdoc/>
        public MeasureBlockLayout Copy()
        {
            return new MeasureBlockLayout(StemLength, BeamAngle);
        }
    }
}
