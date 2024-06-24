using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents an editable grace chord.
    /// </summary>
    public interface IGraceChord : IGraceable, INoteContainer<IGraceNote, IChordLayout>, IScoreElement, IUniqueScoreElement
    {
        /// <summary>
        /// Get the index of the grace chord in the grace group.
        /// </summary>
        int IndexInGroup { get; }
    }
}
