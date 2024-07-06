using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents a chord editor interface.
    /// </summary>
    public interface IChord : INoteContainer<INote>, IChordLayout, IPositionElement, IScoreElement, IUniqueScoreElement
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
