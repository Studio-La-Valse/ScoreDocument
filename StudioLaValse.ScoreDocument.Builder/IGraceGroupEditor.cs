namespace StudioLaValse.ScoreDocument.Builder
{
    /// <summary>
    /// Represents an editable grace group.
    /// </summary>
    public interface IGraceGroupEditor : IMeasureBlockEditor<IGraceChordEditor>, IGraceGroup<IGraceChordEditor>, IScoreElementEditor<IGraceGroupLayout>
    {
        /// <summary>
        /// Append a chord to the end of the measure block.
        /// </summary>
        /// <param name="pitches"></param>
        void AppendChord(params Pitch[] pitches);
    }
}
