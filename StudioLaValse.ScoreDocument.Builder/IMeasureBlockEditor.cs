namespace StudioLaValse.ScoreDocument.Builder
{
    /// <summary>
    /// Represents a generic measure block editor.
    /// </summary>
    /// <typeparam name="TChord"></typeparam>
    public interface IMeasureBlockEditor<TChord> : IMeasureBlock<TChord>
        where TChord : IChord
    {
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

    /// <summary>
    /// Represents a measure block editor.
    /// </summary>
    public interface IMeasureBlockEditor : IMeasureBlockEditor<IChordEditor>, IScoreElementEditor<IMeasureBlockLayout>, IPositionElement
    {
        /// <summary>
        /// Append a chord to the end of the measure block.
        /// </summary>
        /// <param name="rythmicDuration"></param>
        /// <param name="pitches"></param>
        void AppendChord(RythmicDuration rythmicDuration, params Pitch[] pitches);
    }
}
