namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents a generic measure block editor.
    /// Can be implemented by a measure block or a grace block.
    /// </summary>
    public interface IChordContainer<TChord, TNote> where TChord : INoteContainer<TNote> where TNote : IHasPitch
    {
        /// <summary>
        /// Enumerate the chords in the block.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TChord> ReadChords();

        /// <summary>
        /// Clears the content of the measure block.
        /// </summary>
        void Clear();

        /// <summary>
        /// Splice a chord from the specified index.
        /// </summary>
        /// <param name="index"></param>
        void Splice(int index);
    }
}
