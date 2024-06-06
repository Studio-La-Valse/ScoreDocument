namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// Represents a grace chord.
    /// </summary>
    /// <typeparam name="TNote"></typeparam>
    /// <typeparam name="TGrace"></typeparam>
    public interface IGraceChord<TNote, TGrace> : INoteContainer<TNote, TGrace>
    {
        /// <summary>
        /// The index of the chord in the grace group.
        /// </summary>
        int IndexInGroup { get; }
    }
}