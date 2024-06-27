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
        bool OccupySpace { get; set; }

        /// <summary>
        /// Reset the occupy space property to its unset value.
        /// </summary>
        void ResetOccupySpace();

        /// <summary>
        /// The space between chords.
        /// </summary>
        double ChordSpacing { get; set; }

        /// <summary>
        /// Reset the chord spacing to its default unset value.
        /// </summary>
        void ResetChordSpacing();

        /// <summary>
        /// The duration of the chords.
        /// </summary>
        RythmicDuration ChordDuration { get; set; }

        /// <summary>
        /// Reset the chord duration to its default unset value.
        /// </summary>
        void ResetChordDuration();

        /// <summary>
        /// The scale of this grace group.
        /// </summary>
        double Scale { get; set; }

        /// <summary>
        /// Reset the scale to its default unset value.
        /// </summary>
        void ResetScale();
    }
}