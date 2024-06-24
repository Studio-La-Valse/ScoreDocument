using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents a generic chord editor interface.
    /// Can be implemented by a regular chord or a grace chord.
    /// </summary>
    public interface INoteContainer<TNote, TLayout> : IHasLayout<TLayout>, IGraceable where TNote : INoteBase
    {
        /// <summary>
        /// Enumerate the primitives in the chord.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TNote> ReadNotes();

        /// <summary>
        /// Reads the contents of the chord.
        /// </summary>
        /// <returns></returns>
        IEnumerable<KeyValuePair<PowerOfTwo, BeamType>> ReadBeamTypes();
        /// <summary>
        /// Reads the contents of the chord.
        /// </summary>
        /// <returns></returns>
        BeamType? ReadBeamType(PowerOfTwo i);

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
        /// Replaces the content of the chord with the specified pitches. 
        /// </summary>
        /// <param name="pitches"></param>
        void Set(params Pitch[] pitches);
    }
}
