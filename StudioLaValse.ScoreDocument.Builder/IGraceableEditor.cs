namespace StudioLaValse.ScoreDocument.Builder
{
    /// <summary>
    /// A generic graceable chord editor that allows chords and grace chords to work recursively.
    /// </summary>
    public interface IGraceableEditor<TGraceGroup>
    {
        /// <summary>
        /// Prepend a grace chord to this chord.
        /// </summary>
        /// <param name="pitches"></param>
        void Grace(params Pitch[] pitches);

        /// <summary>
        /// Read the grace group preceding this chord.
        /// </summary>
        /// <returns></returns>
        TGraceGroup? ReadGraceGroup();
    }
}
