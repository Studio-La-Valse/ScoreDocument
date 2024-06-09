namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// Represents a grace group.
    /// </summary>
    /// <typeparam name="TChord"></typeparam>
    public interface IGraceGroup<TChord> : IChordContainer<TChord>, IScoreElement
    {
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
