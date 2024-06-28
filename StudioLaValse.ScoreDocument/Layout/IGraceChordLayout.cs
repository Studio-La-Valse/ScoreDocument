namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// A chord layout. May be implemented by a regular chord or a grace chord.
    /// </summary>
    public interface IGraceChordLayout : IHasBeamGroup, ILayout
    {
        /// <summary>
        /// The available space to the right of the chord.
        /// </summary>
        ReadonlyTemplateProperty<double> SpaceRight { get; }
    }
}