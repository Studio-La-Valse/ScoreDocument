using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents a note editor.
    /// </summary>
    public interface INote : INoteLayout, IHasPitch, IPositionElement, IScoreElement, IUniqueScoreElement
    {        

    }
}
