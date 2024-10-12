using StudioLaValse.ScoreDocument.Core;
using StudioLaValse.ScoreDocument.StyleTemplates;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// A chord layout. May be implemented by a regular chord or a grace chord.
    /// </summary>
    public interface IChordLayout : IHasBeamGroup, ILayout
    {
        /// <summary>
        /// The available space to the right of the chord.
        /// </summary>
        TemplateProperty<double> SpaceRight { get; }

        /// <summary>
        /// Stem line thickness.
        /// </summary>
        ReadonlyTemplateProperty<double> StemLineThickness { get; }
    }
}