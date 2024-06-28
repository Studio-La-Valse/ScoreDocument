using StudioLaValse.ScoreDocument.Core;

namespace StudioLaValse.ScoreDocument.Layout
{
    /// <summary>
    /// A generic grace group layout.
    /// Extens a <see cref="IMeasureBlockLayout"/>
    /// </summary>
    public interface IGraceGroupLayout : IMeasureBlockLayout, ILayout
    {
        /// <summary>
        /// Specifies whether the grace group occupies space.
        /// If true, non grace elements will be spaced such that this grace group fits in between.
        /// If false, the positions of regular elements are note altered.
        /// </summary>
        TemplateProperty<bool> OccupySpace { get; }

        /// <summary>
        /// The space between chords.
        /// </summary>
        TemplateProperty<double> ChordSpacing { get;  }

        /// <summary>
        /// The duration of the chords.
        /// </summary>
        TemplateProperty<RythmicDuration> ChordDuration { get; }

        /// <summary>
        /// The scale of this grace group.
        /// </summary>
        TemplateProperty<double> Scale { get; }

        /// <summary>
        /// Get or set the block duration. This does not affect the druation of the containing measure whatsoever.
        /// </summary>
        TemplateProperty<RythmicDuration> BlockDuration { get; }
    }
}