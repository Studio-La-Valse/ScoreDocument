namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// Stem direcition
    /// </summary>
    public enum StemDirection
    {
        /// <summary>
        /// Up. Anchor chord is the highest in the group.
        /// </summary>
        Up = 1,
        /// <summary>
        /// Cross. Anchord chord is the first in the group.
        /// </summary>
        Cross = 0,
        /// <summary>
        /// Down. Anchor chord is the lowest in the group.
        /// </summary>
        Down = -1
    }
}