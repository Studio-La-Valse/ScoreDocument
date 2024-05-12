namespace StudioLaValse.ScoreDocument.Builder
{
    /// <summary>
    /// Represents a chord editor interface.
    /// </summary>
    public interface IChordEditor : IChord<INoteEditor>, IScoreElementEditor<IChordLayout>
    {
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
        /// Sets the offset for this chord. Affects all notes in the chord, not the stems or beams. Nested offsets are applied by adding the values.
        /// </summary>
        /// <param name="offset"></param>
        void SetXOffset(double offset);
    }
}
