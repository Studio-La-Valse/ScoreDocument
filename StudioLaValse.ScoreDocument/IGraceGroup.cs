using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents an editable grace group.
    /// </summary>
    public interface IGraceGroup : IChordContainer<IGraceChord, IGraceNote>, IGraceGroupLayout, IUniqueScoreElement, IScoreElement
    {
        /// <summary>
        /// The target position of the grace group.
        /// </summary>
        Position Target { get; }

        /// <summary>
        /// Get or set the block duration. This does not affect the druation of the containing measure whatsoever.
        /// </summary>
        RythmicDuration BlockDuration { get; set; }

        /// <summary>
        /// The number of grace chords in the group.
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Append a chord to the end of the measure block.
        /// </summary>
        /// <param name="pitches"></param>
        void AppendChord(params Pitch[] pitches);
    }
}
