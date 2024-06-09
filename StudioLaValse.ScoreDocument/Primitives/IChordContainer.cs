namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// An element that contains chords.
    /// </summary>
    /// <typeparam name="TChord"></typeparam>
    public interface IChordContainer<TChord>
    {
        /// <summary>
        /// Enumerate the chords in the block.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TChord> ReadChords();
    }
}
