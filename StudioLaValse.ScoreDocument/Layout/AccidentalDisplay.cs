namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// Accidental display behavior.
    /// </summary>
    public enum AccidentalDisplay
    {
        /// <summary>
        /// Always hide.
        /// </summary>
        ForceOff = -1,
        /// <summary>
        /// Do not change.
        /// </summary>
        Default = 0,
        /// <summary>
        /// Always display accidentals.
        /// </summary>
        ForceAccidental = 1,
        /// <summary>
        /// Always display accidentals and naturals.
        /// </summary>
        ForceNatural = 2
    }
}