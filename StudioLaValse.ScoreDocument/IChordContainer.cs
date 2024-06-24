using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents a generic measure block editor.
    /// Can be implemented by a measure block or a grace block.
    /// </summary>
    public interface IChordContainer<TChord, TLayout> : IHasLayout<TLayout> where TChord : IGraceable
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
        /// <summary>
        /// Sets the stem length for this note group.
        /// </summary>
        /// <param name="stemLength"></param>
        void SetStemLength(double stemLength);
        /// <summary>
        /// Sets the stem angle for this group. Assumes a value in radians. Positive values will result in a downward sloping beam.
        /// </summary>
        /// <param name="angle"></param>
        void SetBeamAngle(double angle);
    }
}
