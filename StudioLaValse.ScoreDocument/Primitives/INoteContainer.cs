namespace StudioLaValse.ScoreDocument.Primitives
{
    /// <summary>
    /// An element that contains notes, like a chord.
    /// </summary>
    /// <typeparam name="TNote"></typeparam>
    /// <typeparam name="TGraceGroup"></typeparam>
    public interface INoteContainer<TNote, TGraceGroup>
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
        /// Read the grace group preceding this chord.
        /// </summary>
        /// <returns></returns>
        TGraceGroup? ReadGraceGroup();
    }
}
