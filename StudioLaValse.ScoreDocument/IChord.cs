using StudioLaValse.ScoreDocument.Layout;

namespace StudioLaValse.ScoreDocument
{
    /// <summary>
    /// Represents a chord editor interface.
    /// </summary>
    public interface IChord : INoteContainer<INote>, IChordLayout, IGraceable, IPositionElement, IScoreElement, IUniqueScoreElement
    {

    }
}
