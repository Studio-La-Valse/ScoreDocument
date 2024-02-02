namespace StudioLaValse.ScoreDocument.Editor
{
    /// <summary>
    /// Represents a measure block editor.
    /// </summary>
    public interface IMeasureBlockEditor
        : IMeasureBlock
    {
        /// <summary>
        /// Edit the chords in the measure block.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IChordEditor> EditChords();


        /// <summary>
        /// Clears the content of the measure block.
        /// </summary>
        void Clear();
        /// <summary>
        /// Append a chord to the end of the measure block.
        /// </summary>
        /// <param name="rythmicDuration"></param>
        void AppendChord(RythmicDuration rythmicDuration);
        /// <summary>
        /// Splice a chord from the specified index.
        /// </summary>
        /// <param name="index"></param>
        void Splice(int index);

        /// <summary>
        /// Applies the layout to the measure block.
        /// </summary>
        /// <param name="layout"></param>
        void ApplyLayout(INoteGroupLayout layout);
        /// <summary>
        /// Reads the layout from the measure block.
        /// </summary>
        /// <returns></returns>
        INoteGroupLayout ReadLayout();


        /// <summary>
        /// Applies beaming information to this measure block.
        /// </summary>
        void Rebeam();
    }
}
