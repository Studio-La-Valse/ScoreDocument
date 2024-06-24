using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents an editable grace group.
    /// </summary>
    public interface IGraceGroup : IChordContainer<IGraceChord, IGraceGroupLayout>, IHasLayout<IGraceGroupLayout>, IScoreElement, IUniqueScoreElement
    {
        /// <summary>
        /// Append a chord to the end of the measure block.
        /// </summary>
        /// <param name="pitches"></param>
        void AppendChord(params Pitch[] pitches);

        /// <summary>
        /// The target position of the grace group.
        /// </summary>
        Position Target { get; }

        /// <summary>
        /// The number of grace chords in the group.
        /// </summary>
        int Length { get; }
    }
}
