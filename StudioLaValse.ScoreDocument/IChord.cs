using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents a chord editor interface.
    /// </summary>
    public interface IChord : IGraceable, INoteContainer<INote, IChordLayout>, IPositionElement, IUniqueScoreElement, IScoreElement
    {
        /// <summary>
        /// Sets the required amount of space on the right of the chord.
        /// </summary>
        /// <param name="spaceRight"></param>
        void SetSpaceRight(double spaceRight);
        /// <summary>
        /// Sets the offset for this chord. Affects all notes in the chord, not the stems or beams. Nested offsets are applied by adding the values.
        /// </summary>
        /// <param name="offset"></param>
        void SetXOffset(double offset);
    }
}
