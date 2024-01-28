namespace StudioLaValse.ScoreDocument.Editor
{
    /// <summary>
    /// Represents a chord editor interface.
    /// </summary>
    public interface IChordEditor : IChord, IPositionElement
    {
        /// <summary>
        /// The index of the chord in the host block.
        /// </summary>
        int IndexInBlock { get; }

        /// <summary>
        /// Edit the notes in the chord.
        /// </summary>
        /// <returns></returns>
        IEnumerable<INoteEditor> EditNotes();

        /// <summary>
        /// Clear the content of the chord.
        /// </summary>
        void Clear();
        /// <summary>
        /// Adds the specified pitches to the chord. If the chord already contains a note with the specified pitch, it will not be added.
        /// </summary>
        /// <param name="pitches"></param>
        void Add(params Pitch[] pitches);
        /// <summary>
        /// Replaces the content of the chord with the specified pitches. If the chord already contains a note with the specified pitch, it will not be added.
        /// </summary>
        /// <param name="pitches"></param>
        void Set(params Pitch[] pitches);

        /// <summary>
        /// Applies the layout to the chord.
        /// </summary>
        /// <param name="layout"></param>
        void ApplyLayout(IChordLayout layout);
        /// <summary>
        /// Reads the current layout of the chord.
        /// </summary>
        /// <returns></returns>
        IChordLayout ReadLayout();
    }
}
