namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// A measure block layout.
    /// </summary>
    public interface IMeasureBlockLayout
    {
        /// <summary>
        /// Get or set the steam direction of the measure block.
        /// </summary>
        StemDirection StemDirection { get; set; }

        /// <summary>
        /// Reset the stem direction to its default unset value.
        /// </summary>
        void ResetStemDirection();

        /// <summary>
        /// Get or set the stemlength of the measure block.
        /// </summary>
        double StemLength { get; set; }

        /// <summary>
        /// Reset the stem length to its default unset value.
        /// </summary>
        void ResetStemLength();

        /// <summary>
        /// Get or set the beam angle of the measure block.
        /// </summary>
        double BeamAngle { get; set; }

        /// <summary>
        /// Reset the beam angle to its default unset value.
        /// </summary>
        void ResetBeamAngle();

        /// <summary>
        /// Get or set the beam thickness of the measure block.
        /// </summary>
        double BeamThickness { get; set; }

        /// <summary>
        /// Reset the beam thickness to its default unset value.
        /// </summary>
        void ResetBeamThickness();

        /// <summary>
        /// Get or set the beam spacing of the measure block.
        /// </summary>
        double BeamSpacing { get; set; }

        /// <summary>
        /// Reset the beam spacing to its default unset value.
        /// </summary>
        void ResetBeamSpacing();
    }
}