namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// A generic graceable chord editor that allows chords and grace chords to work recursively.
    /// May be implemented by a regular chord or a grace chord.
    /// </summary>
    public interface IGraceable
    {
        /// <summary>
        /// Prepend a grace chord to this chord.
        /// </summary>
        /// <param name="rythmicDuration"></param>
        /// <param name="pitches"></param>
        void Grace(RythmicDuration rythmicDuration, params Pitch[] pitches);

        /// <summary>
        /// Read the grace group preceding this chord.
        /// </summary>
        /// <returns></returns>
        IGraceGroup? ReadGraceGroup();
    }
}
