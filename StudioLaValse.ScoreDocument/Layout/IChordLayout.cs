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
        TemplateProperty<double> XOffset { get; }

        /// <summary>
        /// The available space to the right of the chord.
        /// </summary>
        TemplateProperty<double> SpaceRight { get;  }
    }
}