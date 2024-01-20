namespace StudioLaValse.ScoreDocument.Reader
{
    /// <summary>
    /// Represents a chord reader.
    /// </summary>
    public interface IChordReader : IChord, IPositionElement, IUniqueScoreElement
    {
        /// <summary>
        /// The index of the chord in the host measure block.
        /// </summary>
        int IndexInBlock { get; }
        /// <summary>
        /// Reads the context of the chord.
        /// </summary>
        /// <returns></returns>
        IInstrumentMeasureReader ReadContext();
        /// <summary>
        /// Reads the contents of the chord.
        /// </summary>
        /// <returns></returns>
        IEnumerable<INoteReader> ReadNotes();

        /// <summary>
        /// Reads the layout of the chord.
        /// </summary>
        /// <returns></returns>
        IChordLayout ReadLayout();
    }
}
