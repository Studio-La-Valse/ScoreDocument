using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents a measure block editor.
    /// </summary>
    public interface IMeasureBlock : IChordContainer<IChord, IMeasureBlockLayout>, IPositionElement, IScoreElement, IUniqueScoreElement
    {
        /// <summary>
        /// Append a chord to the end of the measure block.
        /// </summary>
        /// <param name="rythmicDuration"></param>
        /// <param name="pitches"></param>
        void AppendChord(RythmicDuration rythmicDuration, params Pitch[] pitches);
    }
}
