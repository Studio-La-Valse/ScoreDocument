namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// The layout of a note group.
    /// </summary>
    public interface INoteGroupLayout : IScoreElementLayout<INoteGroupLayout>
    {
        /// <summary>
        /// The stem length of the first chord in the group.
        /// </summary>
        double StemLength { get; }
        /// <summary>
        /// The angle of the beam of the notegroup.
        /// </summary>
        double BeamAngle { get; }
    }
}
