namespace StudioLaValse.ScoreDocument.Builder
{
    /// <summary>
    /// Represents a measure block editor.
    /// </summary>
    public interface IMeasureBlockEditor : IChordContainerEditor<IChordEditor>, IMeasureBlock<IChordEditor>, IScoreElementEditor<IMeasureBlockLayout>, IPositionElement
    {
        /// <summary>
        /// Append a chord to the end of the measure block.
        /// </summary>
        /// <param name="rythmicDuration"></param>
        /// <param name="pitches"></param>
        void AppendChord(RythmicDuration rythmicDuration, params Pitch[] pitches);
    }
}
