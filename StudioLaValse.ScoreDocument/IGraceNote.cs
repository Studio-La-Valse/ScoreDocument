using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents an editable grace note.
    /// </summary>
    public interface IGraceNote : IGraceNoteLayout, IHasPitch, IScoreElement, IUniqueScoreElement
    {

    }
}
