using StudioLaValse.ScoreDocument.Core;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// A chord layout. May be implemented by a regular chord or a grace chord.
    /// </summary>
    public interface IChordLayout : IHasBeamGroup, ILayout
    {
        /// <summary>
        /// The global offset of this chord.
        /// </summary>
        double XOffset { get; set; }

        /// <summary>
        /// Reset the x offset to its unset value.
        /// </summary>
        void ResetXOffset();

        /// <summary>
        /// The available space to the right of the chord.
        /// </summary>
        double SpaceRight { get; set; }

        /// <summary>
        /// Reset the space right to its unset value.
        /// </summary>
        void ResetSpaceRight();
    }
}